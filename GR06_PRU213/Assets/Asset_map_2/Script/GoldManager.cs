using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;

    [Header("UI hiển thị vàng")]
    public TextMeshProUGUI coinText;  // đổi tên từ goldText → coinText

    [Header("Thiết lập vàng")]
    public int startingGold = 500; // Mặc định 500 gold
    public int currentGold;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Nếu có dữ liệu từ map trước thì dùng nó
        if (PlayerData.Instance != null && PlayerData.Instance.gold > 0)
        {
            currentGold = PlayerData.Instance.gold;
            Debug.Log("Đã lấy Gold từ PlayerData: " + currentGold);
        }
        else
        {
            // Nếu không có thì dùng mặc định như cũ
            if (LevelManager.Instance != null && LevelManager.Instance.CurrentLevel != null)
            {
                currentGold = LevelManager.Instance.CurrentLevel.startingResources;
            }
            else
            {
                currentGold = 500;
            }
        }

        UpdateGoldUI();
    }


    public void AddGold(int amount)
    {
        currentGold += amount;
        UpdateGoldUI();
    }

    public bool SpendGold(int amount)
    {
        if (currentGold >= amount)
        {
            currentGold -= amount;
            UpdateGoldUI();
            return true;
        }

        Debug.Log("Không đủ vàng!");
        return false;
    }

    void UpdateGoldUI()
    {
        if (coinText != null)
            coinText.text = "" + currentGold;
    }
}
