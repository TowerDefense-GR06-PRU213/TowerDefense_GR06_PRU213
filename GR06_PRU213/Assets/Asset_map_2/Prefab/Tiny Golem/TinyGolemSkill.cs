using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(EnemyHealth))]
public class TinyGolemSkill : MonoBehaviour
{
    // Bán kính vùng buff — quái nào đứng trong vòng 4 đơn vị sẽ bị ảnh hưởng
    public float phamVi = 4f;

    // Buff tốc độ kéo dài 4 giây
    public float thoiGianHieuLuc = 4f;

    // Sau khi buff xong thì chờ 6 giây mới buff lại
    public float thoiGianHoiChieu = 6f;

    // Tiny còn hơn 50% máu thì buff +40%, dưới 50% thì buff +65%
    public float heSoTangTocThuong = 1.4f;
    public float heSoTangTocManh = 1.65f;

    public GameObject hieuUngBuff;
    public AudioClip amThanhBuff;
    [Range(0f, 1f)] public float amLuong = 1f;

    private EnemyHealth mauEnemy;
    private AudioSource audioSource;

    // Bộ đếm ngược cooldown — bắt đầu từ 0 để kích hoạt ngay lần đầu
    private float demHoiChieu = 0f;

    void Start()
    {
        mauEnemy = GetComponent<EnemyHealth>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0f;
        }

        demHoiChieu = thoiGianHoiChieu;
    }

    void Update()
    {
        if (mauEnemy == null || mauEnemy.GetCurrentHPPercent() <= 0)
            return;

        // Biến demHoiChieu đếm ngược mỗi frame, hết 6 giây thì gọi hàm BuffDongMinh rồi đặt lại
        demHoiChieu -= Time.deltaTime;
        if (demHoiChieu <= 0f)
        {
            BuffDongMinh();
            demHoiChieu = thoiGianHoiChieu;
        }
    }

    void BuffDongMinh()
    {
        float hpPercent = mauEnemy.GetCurrentHPPercent();

        // Kiểm tra máu để quyết định mức buff — còn khỏe thì x1.4, sắp chết thì x1.65
        float heSoTang = hpPercent > 0.5f ? heSoTangTocThuong : heSoTangTocManh;
        string loaiBuff = hpPercent > 0.5f ? "+40%" : "+65%";

        Debug.Log($"🔥 [TinyGolemSkill] Tiny Golem kích hoạt buff {loaiBuff} (HP: {(hpPercent * 100f):F0}%)");

        if (hieuUngBuff != null)
        {
            GameObject fx = Instantiate(hieuUngBuff, transform.position, Quaternion.identity);
            Destroy(fx, 2f);
        }

        if (amThanhBuff != null)
        {
            audioSource.PlayOneShot(amThanhBuff, amLuong);
        }

        // Hàm OverlapCircleAll vẽ vòng tròn bán kính 4, tìm tất cả quái đứng trong đó để tăng tốc
        Collider2D[] quanhBoss = Physics2D.OverlapCircleAll(transform.position, phamVi);
        foreach (Collider2D col in quanhBoss)
        {
            if (col.CompareTag("Enemy") && col.gameObject != this.gameObject)
            {
                EnemyMovement move = col.GetComponent<EnemyMovement>();
                if (move != null)
                    StartCoroutine(TangTocTamThoi(move, heSoTang));
            }
        }
    }

    System.Collections.IEnumerator TangTocTamThoi(EnemyMovement move, float heSo)
    {
        // Lưu tốc độ gốc, nhân lên theo hệ số buff, chờ 4 giây, rồi trả về tốc độ cũ
        float tocDoGoc = move.speed;
        move.speed = tocDoGoc * heSo;

        yield return new WaitForSeconds(thoiGianHieuLuc);

        // Kiểm tra null phòng trường hợp quái bị giết trước khi hết 4 giây
        if (move != null) move.speed = tocDoGoc;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.5f, 0f, 0.3f);
        Gizmos.DrawWireSphere(transform.position, phamVi);
    }
}
