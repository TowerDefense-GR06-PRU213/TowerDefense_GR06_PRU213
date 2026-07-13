using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [HideInInspector] public EnemyWaveSpawner spawner;
    public enum PathType { A, B }
    public PathType pathType = PathType.A;

    // Zombie và Orc chỉ dùng 2 biến này — tốc độ di chuyển và sát thương gây cho cổng khi đến nơi
    public float speed = 2f;
    public int damageToGate = 1;

    public bool isBoss = false;

    private Transform[] path;
    private Transform target;
    private int waypointIndex = 0;

    void Start()
    {
        // Tùy pathType mà lấy đường đi A hoặc B
        if (pathType == PathType.A)
            path = FindFirstObjectByType<WaypointsA>().points;
        else
            path = FindFirstObjectByType<WaypointsB>().points;

        if (path == null || path.Length == 0)
        {
            Debug.LogWarning("⚠️ Không tìm thấy Waypoints cho " + pathType);
            return;
        }

        transform.position = path[0].position;
        waypointIndex = 0;
        target = path[1];
    }

    void Update()
    {
        if (path == null || target == null) return;

        // Di chuyển về phía điểm tiếp theo theo tốc độ speed
        Vector2 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        // Đến điểm cuối → đánh cổng → tự xóa
        if (waypointIndex >= path.Length - 1)
        {
            GateHealth gate = FindFirstObjectByType<GateHealth>();
            if (gate != null)
            {
                Debug.Log($"⚔️ {name} hit the gate! -{damageToGate} HP");
                gate.TakeDamage(damageToGate);
            }

            if (spawner != null)
                spawner.NotifyEnemyRemoved();

            Destroy(gameObject);
            return;
        }

        waypointIndex++;
        target = path[waypointIndex];
    }
}
