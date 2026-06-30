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
        // Fix: Thêm null check và dùng giá trị mặc định
        if (LevelManager.Instance != null && LevelManager.Instance.CurrentLevel != null)
        {
            currentGold = LevelManager.Instance.CurrentLevel.startingResources;
        }
        else
        {
            Debug.LogWarning("[GoldManager] LevelManager null, dùng giá trị mặc định: 500 gold");
            currentGold = 500; // Mặc định 500 gold
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
