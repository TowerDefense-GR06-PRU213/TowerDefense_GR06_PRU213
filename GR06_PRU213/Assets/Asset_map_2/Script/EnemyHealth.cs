using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyHealth : MonoBehaviour
{
    // 2 sự kiện này là nền tảng để 3 con kia gắn kỹ năng vào
    // Golem dùng OnDamaged, Valkyrie dùng OnTakeDamage
    public event Action OnDamaged;
    public event Func<int, int> OnTakeDamage;

    public int goldReward = 10;

    public int maxHP = 220;
    private int currentHP;

    public GameObject healthBarPrefab;
    private Image healthFill;
    private GameObject healthBarInstance;

    public AudioClip deathSound;
    [Range(0f, 1f)] public float deathSoundVolume = 1f;

    private bool isDead = false;
    public event Action OnEnemyDie;

    void Start()
    {
        // Lúc bắt đầu, máu hiện tại = máu tối đa
        currentHP = maxHP;

        if (healthBarPrefab != null)
        {
            healthBarInstance = Instantiate(
                healthBarPrefab,
                transform.position + Vector3.up * 0.01f,
                Quaternion.identity,
                transform
            );

            Transform fillTransform = healthBarInstance.transform.Find("Background/Fill");
            if (fillTransform != null)
                healthFill = fillTransform.GetComponent<Image>();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        // Nếu có ai đăng ký vào OnTakeDamage (như Valkyrie) thì cho tính lại damage trước
        if (OnTakeDamage != null)
            damage = OnTakeDamage.Invoke(damage);

        // Trừ máu, cập nhật thanh máu
        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;
        UpdateHealthBar();

        // Bắn sự kiện OnDamaged để Golem biết mà kích hoạt kỹ năng
        OnDamaged?.Invoke();

        if (currentHP <= 0)
            Die();
    }

    void UpdateHealthBar()
    {
        // Cập nhật thanh máu theo tỉ lệ máu còn lại
        if (healthFill != null)
            healthFill.fillAmount = (float)currentHP / maxHP;
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        OnEnemyDie?.Invoke();

        Animator anim = GetComponent<Animator>();
        if (anim != null)
            anim.SetTrigger("Die");

        if (deathSound != null)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = deathSound;
            audioSource.volume = deathSoundVolume;
            audioSource.spatialBlend = 0f;
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.Play();
            Destroy(audioSource, deathSound.length + 0.1f);
        }

        // Cộng vàng thưởng cho người chơi khi quái chết
        if (GoldManager.Instance != null)
            GoldManager.Instance.AddGold(goldReward);

        // Xóa quái sau 0.8 giây để animation chết phát xong
        Destroy(gameObject, 0.8f);
    }

    // Trả về % máu còn lại — Tiny Golem dùng để quyết định buff +40% hay +65%
    public float GetCurrentHPPercent()
    {
        return (float)currentHP / maxHP;
    }
}
