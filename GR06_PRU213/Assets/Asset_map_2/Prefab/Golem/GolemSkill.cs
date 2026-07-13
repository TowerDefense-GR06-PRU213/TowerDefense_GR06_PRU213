using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyHealth))]
public class GolemSkill : MonoBehaviour
{
    // Hệ số nhân tốc độ — 1.5 tức nhanh hơn 50%
    public float heSoTangToc = 1.5f;

    // Trạng thái tăng tốc kéo dài 2 giây
    public float thoiGianToc = 2f;

    public float thoiGianHoiChieu = 6f;

    public GameObject hieuUngToc;
    public AudioClip amThanhToc;
    [Range(0f, 1f)] public float amLuong = 0.8f;

    public float doCaoHieuUng = -0.1f;
    public float doSauHieuUng = -0.5f;
    public float heSoPhongTo = 3f;
    public string sortingLayerName = "Enemy";
    public int sortingOrder = 1;

    private EnemyMovement enemyMove;
    private EnemyHealth enemyHealth;

    // Lưu tốc độ gốc để sau 2 giây reset về đúng giá trị cũ
    private float tocDoGoc;

    // Cờ đảm bảo kỹ năng chỉ kích hoạt đúng 1 lần duy nhất
    private bool daKichHoat = false;
    private AudioSource audioSource;

    void Start()
    {
        enemyMove = GetComponent<EnemyMovement>();
        enemyHealth = GetComponent<EnemyHealth>();

        // Lưu lại tốc độ gốc
        tocDoGoc = enemyMove.speed;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0f;
        }

        // Đăng ký lắng nghe sự kiện OnDamaged — khi Golem bị đánh thì gọi hàm KichHoatLanDau
        enemyHealth.OnDamaged += KichHoatLanDau;
    }

    private void KichHoatLanDau()
    {
        // Biến daKichHoat đảm bảo chỉ lần đầu tiên mới kích hoạt
        if (!daKichHoat)
        {
            daKichHoat = true;
            StartCoroutine(KichHoatTangToc());
        }
    }

    IEnumerator KichHoatTangToc()
    {
        if (hieuUngToc != null)
        {
            Vector3 viTriHieuUng = new Vector3(
                transform.position.x,
                transform.position.y + doCaoHieuUng,
                transform.position.z + doSauHieuUng
            );

            GameObject fx = Instantiate(hieuUngToc, viTriHieuUng, Quaternion.identity);
            fx.transform.localScale = Vector3.one * heSoPhongTo;
            fx.transform.SetParent(transform, worldPositionStays: true);
            fx.transform.localPosition = new Vector3(0f, doCaoHieuUng, doSauHieuUng);

            var renderer = fx.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.sortingLayerName = sortingLayerName;
                renderer.sortingOrder = sortingOrder;
            }

            Destroy(fx, thoiGianToc);
        }

        if (amThanhToc != null)
            audioSource.PlayOneShot(amThanhToc, amLuong);

        // Biến speed nhân với 1.5 → chạy nhanh hơn 50% trong 2 giây → reset về tốc độ gốc
        enemyMove.speed = tocDoGoc * heSoTangToc;
        yield return new WaitForSeconds(thoiGianToc);
        enemyMove.speed = tocDoGoc;
    }

    private void OnDestroy()
    {
        // Khi Golem chết thì hủy đăng ký sự kiện — tránh lỗi gọi hàm trên object đã bị xóa
        if (enemyHealth != null)
            enemyHealth.OnDamaged -= KichHoatLanDau;
    }
}
