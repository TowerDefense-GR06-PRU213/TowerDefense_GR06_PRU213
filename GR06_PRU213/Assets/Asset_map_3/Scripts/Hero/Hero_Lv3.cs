using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.Jobs;
using System.Linq;

public class Hero_Lv3 : MonoBehaviour
{
  
    [SerializeField] private HeroData_Lv3 data;
    private ObjectPooler_Lv3 _projectilePool;
    private float _shootTimer; // Đếm ngược thời gian bắn
    private AudioSource audioSource; // "Loa" riêng của trụ này

    // --- THÊM MỚI ---
    private Animator _animator; // Tham chiếu đến Animator
    // --- KẾT THÚC THÊM MỚI ---

    // --- THÊM UPGRADE SYSTEM ---
    private int _currentLevel = 1;
    public int CurrentLevel => _currentLevel;
    public int MaxLevel => data.maxLevel;
    
    // Stats đã được tính với upgrade
    private float _currentDamage;
    private float _currentRange;
    private float _currentShootInterval;
    // --- KẾT THÚC UPGRADE SYSTEM ---

    void Awake()
    {
        // Mỗi trụ nên có AudioSource riêng
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1.0f; // Để âm thanh 3D (phát ra từ trụ)
    }

    private void Start()
    {
        _projectilePool = GetComponent<ObjectPooler_Lv3>();
        _shootTimer = 0; // Bắn ngay khi bắt đầu

        // --- THÊM MỚI ---
        // Lấy component Animator trên cùng GameObject này
        _animator = GetComponent<Animator>();
        // --- KẾT THÚC THÊM MỚI ---

        // --- KHỞI TẠO STATS BAN ĐẦU ---
        InitializeStats();
        // --- KẾT THÚC ---
    }

    /// <summary>
    /// Khởi tạo stats ban đầu (level 1)
    /// </summary>
    private void InitializeStats()
    {
        _currentLevel = 1;
        _currentDamage = data.damage;
        _currentRange = data.range;
        _currentShootInterval = data.shootInterval;
    }

    private void Update()
    {
        _shootTimer -= Time.deltaTime;
        if (_shootTimer <= 0)
        {
            // Hàm Shoot() sẽ tự xử lý việc reset _shootTimer
            Shoot();
        }
    }

    private void OnDrawGizmos()
    {
        if (data == null) return;
        Gizmos.color = Color.green;
        // Hiển thị range hiện tại (đã tính upgrade)
        float rangeToShow = Application.isPlaying ? _currentRange : data.range;
        Gizmos.DrawWireSphere(transform.position, rangeToShow);
    }

    /// <summary>
    /// Hàm bắn chính, kết hợp logic tìm địch, buff, và reset timer
    /// </summary>
    private void Shoot()
    {
        // 1. Lấy tất cả quái
        List<Enemy_Lv3> allEnemies = EnemyManager_Lv3.Instance.GetActiveEnemies();

        // Mặc định reset timer. Nếu không có quái, chờ 1 nhịp.
        // (Bạn có thể đổi thành data.shootInterval nếu muốn)
        float nextShootInterval = 0.25f;

        if (allEnemies == null || allEnemies.Count == 0)
        {
            _shootTimer = nextShootInterval;
            // --- THÊM MỚI ---
            // Không có quái, đặt trạng thái là "Không tấn công" (Idle)
            _animator.SetBool("IsAttacking_Lv3", false);
            // ---- DEBUG LOG 1 ----
            Debug.Log("ANIM_DEBUG: Không có quái nào. (Setting false)");
            // ---------------------
            return;
            // --- KẾT THÚC THÊM MỚI ---
            return;
        }

        // 2. Lọc quái trong tầm bắn (dùng range đã upgrade)
        List<Enemy_Lv3> inRangeEnemies = new List<Enemy_Lv3>();
        foreach (var e in allEnemies)
        {
            if (e == null || !e.gameObject.activeInHierarchy) continue;
            float dist = Vector3.Distance(transform.position, e.transform.position);
            if (dist <= _currentRange)
                inRangeEnemies.Add(e);
        }

        if (inRangeEnemies.Count == 0)
        {
            _shootTimer = nextShootInterval;
            // --- THÊM MỚI ---
            // Không có quái trong tầm, đặt trạng thái là "Không tấn công" (Idle)
            _animator.SetBool("IsAttacking_Lv3", false);
            // ---- DEBUG LOG 2 ----
            Debug.Log("ANIM_DEBUG: Có quái, nhưng không con nào trong tầm. (Setting false)");
            // ---------------------
            // --- KẾT THÚC THÊM MỚI ---
            return;
        }

        // 3. Chọn mục tiêu dựa trên ưu tiên (Smart, First, v.v.)
        Enemy_Lv3 target = SelectTarget(inRangeEnemies);
        if (target == null)
        {
            _shootTimer = nextShootInterval;
            // --- THÊM MỚI ---
            // Không tìm thấy mục tiêu (ví dụ: logic Smart không tìm thấy), đặt là Idle
            _animator.SetBool("IsAttacking_Lv3", false);

            // ---- DEBUG LOG 3 ----
            Debug.Log("ANIM_DEBUG: Không chọn được mục tiêu (ví dụ: Smart logic). (Setting false)");
            // ---------------------
            // --- KẾT THÚC THÊM MỚI ---
            return;
        }

        // --- THÊM MỚI ---
        // ĐÃ TÌM THẤY MỤC TIÊU! Đặt trạng thái là "Đang tấn công"
        _animator.SetBool("IsAttacking_Lv3", true);
        // --- KẾT THÚC THÊM MỚI ---
        // ---- DEBUG LOG 4 ----
        Debug.Log("ANIM_DEBUG: ĐÃ TÌM THẤY MỤC TIÊU! (Setting TRUE)");
        // -----------

        // 4. Tính toán Sát thương và Tốc độ đánh (đã bao gồm upgrade)
        float finalDamage = _currentDamage;
        float finalShootInterval = _currentShootInterval;

        // Nếu là trụ băng VÀ bắn quái băng -> được buff
        if (data.isMapIceHero && IsIceEnemy(target))
        {
            finalDamage *= data.iceDamageMultiplier;
            finalShootInterval /= data.iceAttackSpeedMultiplier; // Bắn nhanh hơn
        }

        // 5. Reset timer cho phát bắn TIẾP THEO
        _shootTimer = finalShootInterval;

        // 6. Phát âm thanh (Lấy từ Scriptable Object)
        if (data.shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(data.shootSound);
        }
        // 6. Bắn đạn
        GameObject projectile = _projectilePool.GetPoolObject();
        if (projectile == null) return;

        projectile.transform.position = transform.position;
        projectile.SetActive(true);

        Vector2 dir = (target.transform.position - transform.position).normalized;

        // Giả định hàm shoot của bạn có 3 tham số
        projectile.GetComponent<Projecile_Lv3>().shoot(data, dir, finalDamage);
    }

    /// <summary>
    /// Chọn mục tiêu từ danh sách quái trong tầm bắn
    /// </summary>
    private Enemy_Lv3 SelectTarget(List<Enemy_Lv3> list)
    {
        // --- LOGIC "SMART" (TÙY TÌNH HUỐNG) ---
        if (data.targetPriority == TargetPriority.Smart)
        {
            Enemy_Lv3 weakestEnemy = null;
            float minHealth = float.MaxValue;

            // Tình huống 1: Tìm quái để "kết liễu" (còn dưới 25% máu)
            foreach (Enemy_Lv3 e in list)
            {
                float healthPercent = e.currentHealth / e.Data.lives;
                if (healthPercent > 0 && healthPercent <= 0.25f)
                {
                    if (e.currentHealth < minHealth)
                    {
                        minHealth = e.currentHealth;
                        weakestEnemy = e;
                    }
                }
            }

            // Nếu tìm thấy một con để kết liễu -> Bắn nó ngay
            if (weakestEnemy != null)
            {
                return weakestEnemy;
            }

            // Tình huống 2: Nếu không có con nào sắp chết, quay về logic "First"
            return GetTargetByPriority(list, TargetPriority.First);
        }

        // --- LOGIC CƠ BẢN (First, Closest, v.v.) ---
        return GetTargetByPriority(list, data.targetPriority);
    }

    /// <summary>
    /// Sắp xếp danh sách và trả về mục tiêu đầu tiên
    /// </summary>
    private Enemy_Lv3 GetTargetByPriority(List<Enemy_Lv3> list, TargetPriority priority)
    {
        // --- SỬA LỖI LOGIC ---
        // Code của bạn dùng: switch (data.targetPriority)
        // Đã sửa thành: switch (priority)
        // (Để logic "Smart" có thể gọi "First" một cách chính xác)
        switch (priority)
        {
            case TargetPriority.Closest:
                list.Sort((a, b) =>
                    Vector3.Distance(transform.position, a.transform.position)
                    .CompareTo(Vector3.Distance(transform.position, b.transform.position)));
                break;

            case TargetPriority.Strongest:
                list.Sort((a, b) => b.currentHealth.CompareTo(a.currentHealth));
                break;

            case TargetPriority.Weakest:
                list.Sort((a, b) => a.currentHealth.CompareTo(b.currentHealth));
                break;

            case TargetPriority.First:
            default:
                list.Sort((a, b) => b.distanceTravelled.CompareTo(a.distanceTravelled));
                break;
        }

        if (list.Count > 0)
        {
            return list[0]; // Trả về phần tử đầu tiên sau khi đã sắp xếp
        }
        return null;
    }

    /// <summary>
    /// Kiểm tra xem quái có phải loại "Băng" không
    /// </summary>
    private bool IsIceEnemy(Enemy_Lv3 enemy)
    {
        if (enemy == null || enemy.Data == null) return false;

        return enemy.Data.type == EnemyType_Lv3.yeti ||
               enemy.Data.type == EnemyType_Lv3.YetiTanker ||
               enemy.Data.type == EnemyType_Lv3.PhuThuyBang ||
               enemy.Data.type == EnemyType_Lv3.SnowMan ||
               enemy.Data.type == EnemyType_Lv3.BossYeti;
    }

    // --- THÊM CÁC HÀM CHO HỆ THỐNG NÂNG CẤP ---

    /// <summary>
    /// Kiểm tra xem tướng có thể nâng cấp không
    /// </summary>
    public bool CanUpgrade()
    {
        return _currentLevel < data.maxLevel;
    }

    /// <summary>
    /// Lấy giá nâng cấp hiện tại
    /// </summary>
    public int GetUpgradeCost()
    {
        if (!CanUpgrade()) return 0;
        // Có thể tăng giá theo level: cost * level
        return data.upgradeCost * _currentLevel;
    }

    /// <summary>
    /// Nâng cấp tướng lên 1 level
    /// </summary>
    public void Upgrade()
    {
        if (!CanUpgrade())
        {
            Debug.LogWarning($"{name} đã đạt level tối đa!");
            return;
        }

        _currentLevel++;

        // Tính toán stats mới
        RecalculateStats();

        Debug.Log($"{name} đã nâng cấp lên Level {_currentLevel}! " +
                  $"Damage: {_currentDamage:F1}, Range: {_currentRange:F1}, " +
                  $"Shoot Interval: {_currentShootInterval:F2}s");

        // TODO: Thêm hiệu ứng visual khi upgrade (particle, sound, v.v.)
    }

    /// <summary>
    /// Tính toán lại stats dựa trên level hiện tại
    /// </summary>
    private void RecalculateStats()
    {
        // Level 1 = stats gốc
        // Level 2 = stats gốc * (1 + multiplier)
        // Level 3 = stats gốc * (1 + multiplier * 2)
        int levelsAboveOne = _currentLevel - 1;

        _currentDamage = data.damage * (1 + data.damageUpgradeMultiplier * levelsAboveOne);
        _currentRange = data.range * (1 + data.rangeUpgradeMultiplier * levelsAboveOne);
        _currentShootInterval = data.shootInterval * (1 - data.shootSpeedUpgradeMultiplier * levelsAboveOne);

        // Đảm bảo shoot interval không âm
        if (_currentShootInterval < 0.1f) _currentShootInterval = 0.1f;
    }

    /// <summary>
    /// Lấy stats hiện tại (để hiển thị UI)
    /// </summary>
    public (float damage, float range, float shootInterval) GetCurrentStats()
    {
        return (_currentDamage, _currentRange, _currentShootInterval);
    }

    /// <summary>
    /// Lấy stats sau khi nâng cấp (để preview)
    /// </summary>
    public (float damage, float range, float shootInterval) GetNextLevelStats()
    {
        if (!CanUpgrade()) return GetCurrentStats();

        int nextLevel = _currentLevel;
        float nextDamage = data.damage * (1 + data.damageUpgradeMultiplier * nextLevel);
        float nextRange = data.range * (1 + data.rangeUpgradeMultiplier * nextLevel);
        float nextShootInterval = data.shootInterval * (1 - data.shootSpeedUpgradeMultiplier * nextLevel);

        if (nextShootInterval < 0.1f) nextShootInterval = 0.1f;

        return (nextDamage, nextRange, nextShootInterval);
    }

    /// <summary>
    /// Lấy thông tin hero data
    /// </summary>
    public HeroData_Lv3 GetData()
    {
        return data;
    }

    // --- KẾT THÚC HỆ THỐNG NÂNG CẤP ---
}

// Bạn cũng cần định nghĩa enum TargetPriority ở đâu đó,
// ví dụ: bên ngoài class Hero hoặc trong file riêng
public enum TargetPriority
{
    First,
    Closest,
    Strongest,
    Weakest,
    Smart
}

 
