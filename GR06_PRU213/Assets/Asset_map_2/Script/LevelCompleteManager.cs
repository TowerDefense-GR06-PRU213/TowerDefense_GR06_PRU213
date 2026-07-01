using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteManager : MonoBehaviour
{
    [Header("UI hiển thị khi thắng (root)")]
    public GameObject uiNextLevel;

    [Header("Controller của MissionCompletePanel")]
    public UI_NextLevelController controller; // 👈 thêm biến toàn cục

    [Header("Tên scene của các map kế tiếp (theo thứ tự)")]
    public string[] sceneNames; // Ví dụ: Game_Map1, Game_Map2, Game_Map3, Game_Map4

    private int currentSceneIndex;
    private bool shown = false; // chặn double click

    void Start()
    {
        if (uiNextLevel != null) uiNextLevel.SetActive(false);
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Nếu chưa kéo thủ công thì tự tìm trong children (kể cả inactive)
        if (controller == null && uiNextLevel != null)
            controller = uiNextLevel.GetComponentInChildren<UI_NextLevelController>(true);
    }

    void Update()
    {
        if (!shown && Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("▶ Nhấn W: gọi ShowWinUI()");
            ShowWinUI();
        }
    }

    public void ShowWinUI()
    {
        if (shown) return;
        shown = true;

        if (uiNextLevel != null)
            uiNextLevel.SetActive(true);

        // Gọi UI hiển thị MissionCompletePanel
        if (controller != null)
        {
            controller.ShowMissionComplete();
            Debug.Log("🏆 Hiện MissionCompletePanel NGAY lập tức!");
        }
        else
        {
            Debug.LogWarning("⚠ Không tìm thấy UI_NextLevelController!");
        }

        // Dừng game sau khi hiện UI
        Time.timeScale = 0f;
    }

    public void NextLevel()
    {
        // Nếu chưa có PlayerData thì tự tạo
        if (PlayerData.Instance == null)
        {
            GameObject obj = new GameObject("PlayerData");
            obj.AddComponent<PlayerData>();

            Debug.Log("Auto created PlayerData");
        }

        Time.timeScale = 1f;

        // Save Gold
        if (GoldManager.Instance != null)
        {
            PlayerData.Instance.gold = GoldManager.Instance.currentGold;
            Debug.Log("Saved Gold = " + PlayerData.Instance.gold);
        }

        // Save Lives
        GateHealth gate = FindObjectOfType<GateHealth>();
        if (gate != null)
        {
            PlayerData.Instance.lives = gate.currentHealth;
            Debug.Log("Saved Lives = " + PlayerData.Instance.lives);
        }

        SceneManager.LoadScene("Game_Map3");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
