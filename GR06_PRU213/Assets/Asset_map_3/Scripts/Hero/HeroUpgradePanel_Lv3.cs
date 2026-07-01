using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Panel UI để nâng cấp và bán tướng
/// </summary>
public class HeroUpgradePanel_Lv3 : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject panelObject;
    [SerializeField] private TextMeshProUGUI heroNameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI currentStatsText;
    [SerializeField] private TextMeshProUGUI nextStatsText;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button sellButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI upgradeCostText;
    [SerializeField] private TextMeshProUGUI sellValueText;

    [Header("Settings")]
    [SerializeField] private float sellRefundPercentage = 0.7f; // Bán được 70% giá trị

    private Hero_Lv3 _currentHero;
    private Platform_Lv3 _currentPlatform;

    private void Awake()
    {
        // Ẩn panel khi bắt đầu
        if (panelObject != null)
            panelObject.SetActive(false);

        // Đăng ký sự kiện cho các button
        if (upgradeButton != null)
            upgradeButton.onClick.AddListener(OnUpgradeClicked);
        
        if (sellButton != null)
            sellButton.onClick.AddListener(OnSellClicked);
        
        if (closeButton != null)
            closeButton.onClick.AddListener(OnCloseClicked);
    }

    /// <summary>
    /// Hiển thị panel với thông tin tướng
    /// </summary>
    public void ShowPanel(Hero_Lv3 hero, Platform_Lv3 platform)
    {
        if (hero == null || platform == null)
        {
            Debug.LogError("Hero hoặc Platform null!");
            return;
        }

        _currentHero = hero;
        _currentPlatform = platform;

        UpdateUI();

        if (panelObject != null)
            panelObject.SetActive(true);

        // Pause game khi mở panel (tùy chọn)
        // Time.timeScale = 0f;
    }

    /// <summary>
    /// Ẩn panel
    /// </summary>
    public void HidePanel()
    {
        if (panelObject != null)
            panelObject.SetActive(false);

        _currentHero = null;
        _currentPlatform = null;

        // Resume game (nếu đã pause)
        // Time.timeScale = 1f;
    }

    /// <summary>
    /// Cập nhật toàn bộ UI
    /// </summary>
    private void UpdateUI()
    {
        if (_currentHero == null) return;

        HeroData_Lv3 data = _currentHero.GetData();
        
        // Tên tướng
        if (heroNameText != null)
            heroNameText.text = data.name.Replace("_", " ");

        // Level
        if (levelText != null)
            levelText.text = $"Level {_currentHero.CurrentLevel}/{_currentHero.MaxLevel}";

        // Stats hiện tại
        var currentStats = _currentHero.GetCurrentStats();
        if (currentStatsText != null)
        {
            currentStatsText.text = $"<b>Current Stats:</b>\n" +
                                   $"Damage: {currentStats.damage:F1}\n" +
                                   $"Range: {currentStats.range:F1}\n" +
                                   $"Attack Speed: {(1f/currentStats.shootInterval):F2}/s";
        }

        // Stats sau khi nâng cấp
        bool canUpgrade = _currentHero.CanUpgrade();
        if (nextStatsText != null)
        {
            if (canUpgrade)
            {
                var nextStats = _currentHero.GetNextLevelStats();
                nextStatsText.text = $"<b>Next Level:</b>\n" +
                                    $"Damage: {nextStats.damage:F1} <color=green>(+{(nextStats.damage - currentStats.damage):F1})</color>\n" +
                                    $"Range: {nextStats.range:F1} <color=green>(+{(nextStats.range - currentStats.range):F1})</color>\n" +
                                    $"Attack Speed: {(1f/nextStats.shootInterval):F2}/s <color=green>(+{((1f/nextStats.shootInterval) - (1f/currentStats.shootInterval)):F2})</color>";
            }
            else
            {
                nextStatsText.text = "<b><color=yellow>MAX LEVEL</color></b>";
            }
        }

        // Button upgrade
        if (upgradeButton != null)
        {
            upgradeButton.interactable = canUpgrade;
            
            if (upgradeCostText != null)
            {
                if (canUpgrade)
                {
                    int cost = _currentHero.GetUpgradeCost();
                    upgradeCostText.text = $"Upgrade ({cost} Gold)";
                    
                    // Kiểm tra đủ tiền không
                    if (CurrencyManager_Lv3.Instance != null)
                    {
                        bool canAfford = CurrencyManager_Lv3.Instance.CurrentGold >= cost;
                        upgradeButton.interactable = canAfford;
                        upgradeCostText.color = canAfford ? Color.white : Color.red;
                    }
                }
                else
                {
                    upgradeCostText.text = "MAX LEVEL";
                }
            }
        }

        // Button sell
        if (sellButton != null && sellValueText != null)
        {
            int sellValue = CalculateSellValue();
            sellValueText.text = $"Sell ({sellValue} Gold)";
        }
    }

    /// <summary>
    /// Tính giá trị bán tướng
    /// </summary>
    private int CalculateSellValue()
    {
        if (_currentHero == null) return 0;

        HeroData_Lv3 data = _currentHero.GetData();
        int totalInvestment = data.cost;

        // Cộng thêm tiền đã nâng cấp
        for (int i = 1; i < _currentHero.CurrentLevel; i++)
        {
            totalInvestment += data.upgradeCost * i;
        }

        // Bán được % giá trị
        return Mathf.RoundToInt(totalInvestment * sellRefundPercentage);
    }

    /// <summary>
    /// Xử lý khi click nút Upgrade
    /// </summary>
    private void OnUpgradeClicked()
    {
        if (_currentHero == null || !_currentHero.CanUpgrade()) return;

        int cost = _currentHero.GetUpgradeCost();

        // Kiểm tra đủ tiền
        if (CurrencyManager_Lv3.Instance == null)
        {
            Debug.LogError("Không tìm thấy CurrencyManager!");
            return;
        }

        if (CurrencyManager_Lv3.Instance.CurrentGold < cost)
        {
            Debug.Log("Không đủ vàng để nâng cấp!");
            // TODO: Hiển thị thông báo lỗi cho người chơi
            return;
        }

        // Trừ tiền
        CurrencyManager_Lv3.Instance.SpendGold(cost);

        // Nâng cấp
        _currentHero.Upgrade();

        // Cập nhật lại UI
        UpdateUI();

        // TODO: Phát âm thanh upgrade
    }

    /// <summary>
    /// Xử lý khi click nút Sell
    /// </summary>
    private void OnSellClicked()
    {
        if (_currentHero == null || _currentPlatform == null) return;

        int sellValue = CalculateSellValue();

        // Hoàn tiền
        if (CurrencyManager_Lv3.Instance != null)
        {
            CurrencyManager_Lv3.Instance.AddGold(sellValue);
        }

        // Gọi hàm bán của platform
        _currentPlatform.SellHero();

        // Đóng panel
        HidePanel();

        Debug.Log($"Đã bán tướng với giá {sellValue} vàng!");

        // TODO: Phát âm thanh bán
    }

    /// <summary>
    /// Xử lý khi click nút Close
    /// </summary>
    private void OnCloseClicked()
    {
        HidePanel();
    }
}
