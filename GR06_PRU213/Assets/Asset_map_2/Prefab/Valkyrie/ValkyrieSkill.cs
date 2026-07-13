using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class ValkyrieSkill : MonoBehaviour
{
    // Nhân 0.8 = giảm 20% sát thương nhận vào
    public float heSoGiamSatThuong = 0.8f;

    // Khiên kéo dài 2.5 giây
    public float thoiGianHieuLuc = 2.5f;

    // Hết khiên thì chờ 6 giây mới dùng lại
    public float thoiGianHoiChieu = 6f;

    public GameObject spriteKhien;

    private EnemyHealth mauEnemy;

    // 2 cờ trạng thái — đang bật khiên hay đang chờ cooldown
    private bool dangGiamSatThuong = false;
    private bool dangHoiChieu = false;
    private float demHieuLuc = 0f;
    private float demHoiChieu = 0f;

    void Awake()
    {
        mauEnemy = GetComponent<EnemyHealth>();

        // Đăng ký hàm XuLySatThuong vào sự kiện OnTakeDamage
        // Mỗi lần Valkyrie nhận đòn, sát thương phải đi qua hàm này trước rồi mới trừ máu
        mauEnemy.OnTakeDamage += XuLySatThuong;

        if (spriteKhien != null)
            spriteKhien.SetActive(false);
    }

    void OnDestroy()
    {
        // Khi Valkyrie chết thì hủy đăng ký sự kiện — tránh lỗi gọi hàm trên object đã bị xóa
        if (mauEnemy != null)
            mauEnemy.OnTakeDamage -= XuLySatThuong;
    }

    void Update()
    {
        if (dangGiamSatThuong)
        {
            // Đếm ngược 2.5 giây hiệu lực khiên
            demHieuLuc -= Time.deltaTime;
            if (demHieuLuc <= 0f)
            {
                // Hết khiên → bật cooldown 6 giây
                dangGiamSatThuong = false;
                dangHoiChieu = true;
                demHoiChieu = thoiGianHoiChieu;

                if (spriteKhien != null)
                    spriteKhien.SetActive(false);

                Debug.Log("🛡️ Valkyrie hết khiên, bắt đầu hồi chiêu!");
            }
        }

        if (dangHoiChieu)
        {
            // Đếm ngược 6 giây cooldown, hết thì cho phép bật khiên lại
            demHoiChieu -= Time.deltaTime;
            if (demHoiChieu <= 0f)
            {
                dangHoiChieu = false;
                Debug.Log("✨ Kỹ năng của Valkyrie sẵn sàng kích hoạt lại!");
            }
        }
    }

    private int XuLySatThuong(int damageGoc)
    {
        int satThuongSauCung = damageGoc;

        if (!dangHoiChieu)
        {
            if (!dangGiamSatThuong)
            {
                // Bật khiên lên, bắt đầu đếm 2.5 giây
                dangGiamSatThuong = true;
                demHieuLuc = thoiGianHieuLuc;

                if (spriteKhien != null)
                {
                    spriteKhien.SetActive(true);
                    StartCoroutine(PhongToKhien(spriteKhien.transform));
                }

                Debug.Log($"🛡️ Valkyrie kích hoạt giảm sát thương trong {thoiGianHieuLuc}s!");
            }

            // damageGoc nhân 0.8 — bắn 100 thì chỉ nhận 80
            if (dangGiamSatThuong)
                satThuongSauCung = Mathf.RoundToInt(damageGoc * heSoGiamSatThuong);
        }

        return satThuongSauCung;
    }

    private System.Collections.IEnumerator PhongToKhien(Transform target)
    {
        float elapsed = 0f;
        float speed = 5f;
        float maxScale = 5f;
        Vector3 baseScale = Vector3.one;

        while (elapsed < thoiGianHieuLuc)
        {
            float t = Mathf.PingPong(elapsed * speed, 1f);
            float scale = Mathf.Lerp(1f, maxScale, t);
            target.localScale = new Vector3(scale, scale, 1f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        target.localScale = baseScale;
    }
}
