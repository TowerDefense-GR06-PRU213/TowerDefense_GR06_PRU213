//using UnityEngine;
//using UnityEngine.UI;

//public class HeroSelectionUI : MonoBehaviour
//{
//    [Header("Thông báo")]
//    public TMPro.TextMeshProUGUI notEnoughGoldText;

//    public static HeroSelectionUI Instance;

//    private PlatformClick currentPlatform;
//    private bool isPlacingHero = false;
//    private GameObject heroToPlace;

//    [Header("Danh sách tướng")]
//    public GameObject[] heroPrefabs;
//    public int[] heroCosts;

//    [Header("UI Components")]
//    public Button closeButton;
//    public Button[] buyButtons;

//    void Awake()
//    {
//        Instance = this;

//        if (closeButton != null)
//            closeButton.onClick.AddListener(Close);

//        for (int i = 0; i < buyButtons.Length; i++)
//        {
//            int index = i;
//            buyButtons[i].onClick.AddListener(() => SelectHero(index));
//        }

//        gameObject.SetActive(false);
//    }

//    public void ShowAtPlatform(PlatformClick platform)
//    {
//        currentPlatform = platform;
//        gameObject.SetActive(true);
//        Time.timeScale = 0f;
//    }

//    public void ShowFromButton()
//    {
//        currentPlatform = null;
//        gameObject.SetActive(true);
//        Time.timeScale = 0f;
//    }

//    public void SelectHero(int index)
//    {
//        int cost = heroCosts[index];
//        if (GoldManager.Instance != null && GoldManager.Instance.SpendGold(cost))
//        {
//            // ✅ Gọi HeroPlacementManager để xử lý đặt tướng
//            if (HeroPlacementManager.Instance != null)
//            {
//                HeroPlacementManager.Instance.StartPlacing(heroPrefabs[index]);
//            }

//            // Đóng UI và resume game
//            gameObject.SetActive(false);
//            Time.timeScale = 1f;
//            Debug.Log("🧩 Đã mua tướng, chờ người chơi đặt");
//        }
//        else
//        {
//            Debug.Log("❌ Không đủ vàng!");
//            ShowNotEnoughGold();
//        }
//    }



//    void Update()
//    {
//        if (isPlacingHero && heroToPlace != null)
//        {
//            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            mouseWorld.z = 0;
//            heroToPlace.transform.position = mouseWorld;
//            heroToPlace.SetActive(true);

//            // ✅ Khi click chuột trái → đặt tướng
//            if (Input.GetMouseButtonDown(0))
//            {
//                isPlacingHero = false;
//                heroToPlace = null;
//                Debug.Log("✅ Đã đặt tướng!");
//            }
//        }
//    }

//    private void ShowNotEnoughGold()
//    {
//        if (notEnoughGoldText == null) return;
//        StopAllCoroutines(); // ngắt thông báo cũ (nếu có)
//        StartCoroutine(ShowGoldWarning());
//    }

//    private System.Collections.IEnumerator ShowGoldWarning()
//    {
//        notEnoughGoldText.gameObject.SetActive(true);
//        yield return new WaitForSecondsRealtime(2f); // chờ 2 giây (không phụ thuộc Time.timeScale)
//        notEnoughGoldText.gameObject.SetActive(false);
//    }


//    public void Close()
//    {
//        gameObject.SetActive(false);
//        currentPlatform = null;
//        Time.timeScale = 1f;

//        // 🔹 Ẩn thông báo "Không đủ vàng" khi đóng UI
//        if (notEnoughGoldText != null)
//            notEnoughGoldText.gameObject.SetActive(false);
//    }

//}
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroSelectionUI : MonoBehaviour
{
    [Header("Thông báo")]
    public TMPro.TextMeshProUGUI notEnoughGoldText;

    public static HeroSelectionUI Instance;
    private PlatformClick currentPlatform;

    [Header("Danh sách tướng")]
    public GameObject[] heroPrefabs;
    public int[] heroCosts;
    public string[] heroNames; // Tên hiển thị của mỗi hero

    [Header("UI Components")]
    public Button closeButton;
    public Button[] buyButtons;
    
    [Header("Hero Info Text (Tên + Giá)")]
    public TextMeshProUGUI[] heroNameTexts;   // Text hiển thị tên hero
    public TextMeshProUGUI[] heroCostTexts;   // Text hiển thị giá tiền
    
    [Header("Nút xóa tướng")]
    public GameObject removePanel;       // panel nhỏ có nút xóa
    public Button removeButton;

    void Awake()
    {
        Instance = this;

        if (closeButton != null)
            closeButton.onClick.AddListener(Close);

        for (int i = 0; i < buyButtons.Length; i++)
        {
            int index = i;
            buyButtons[i].onClick.AddListener(() => SelectHero(index));
        }

        if (removeButton != null)
            removeButton.onClick.AddListener(RemoveHero);

        // FIX: Make close button visible
        FixCloseButton();
        
        // Populate hero info UI
        UpdateHeroInfoUI();

        gameObject.SetActive(false);
        if (removePanel != null)
            removePanel.SetActive(false);
    }
    
    // FIX: Làm nút close button hiển thị được
    private void FixCloseButton()
    {
        if (closeButton != null)
        {
            Image closeImg = closeButton.GetComponent<Image>();
            if (closeImg != null)
            {
                // Đổi màu nút close thành màu đỏ và hiển thị rõ
                closeImg.color = new Color(0.8f, 0.2f, 0.2f, 1f); // Màu đỏ, alpha = 1
                Debug.Log("[HeroSelectionUI] Đã sửa close button - màu đỏ alpha=1");
            }
            
            // Nếu có text "X" trong button
            TextMeshProUGUI closeText = closeButton.GetComponentInChildren<TextMeshProUGUI>();
            if (closeText != null)
            {
                closeText.text = "X";
                closeText.color = Color.white;
                closeText.fontSize = 40;
                closeText.fontStyle = FontStyles.Bold;
                Debug.Log("[HeroSelectionUI] Đã set text X cho close button");
            }
        }
    }
    
    // Hàm cập nhật tên hero và giá tiền lên UI
    private void UpdateHeroInfoUI()
    {
        // Cập nhật tên hero
        if (heroNameTexts != null && heroNames != null)
        {
            for (int i = 0; i < heroNameTexts.Length && i < heroNames.Length; i++)
            {
                if (heroNameTexts[i] != null)
                {
                    heroNameTexts[i].text = heroNames[i];
                    heroNameTexts[i].color = Color.black; // Đảm bảo text màu đen
                    heroNameTexts[i].fontSize = 28; // Tăng size
                    heroNameTexts[i].fontStyle = FontStyles.Bold; // In đậm
                }
            }
        }
        
        // Cập nhật giá tiền
        if (heroCostTexts != null && heroCosts != null)
        {
            for (int i = 0; i < heroCostTexts.Length && i < heroCosts.Length; i++)
            {
                if (heroCostTexts[i] != null)
                {
                    heroCostTexts[i].text = heroCosts[i].ToString();
                    heroCostTexts[i].color = new Color(1f, 0.84f, 0f); // Màu vàng cho gold
                    heroCostTexts[i].fontSize = 24;
                    heroCostTexts[i].fontStyle = FontStyles.Bold;
                }
            }
        }
    }

    // 🟢 Gọi khi nhấn platform trống
    public void ShowAtPlatform(PlatformClick platform)
    {
        currentPlatform = platform;
        gameObject.SetActive(true);
        if (removePanel != null) removePanel.SetActive(false);
        Time.timeScale = 0f;
    }

    // 🟠 Gọi khi nhấn platform có tướng
    public void ShowRemoveUI(PlatformClick platform)
    {
        currentPlatform = platform;
        gameObject.SetActive(true);
        if (removePanel != null) removePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SelectHero(int index)
    {
        int cost = heroCosts[index];
        if (GoldManager.Instance != null && GoldManager.Instance.SpendGold(cost))
        {
            if (currentPlatform != null)
            {
                if (currentPlatform.placedHero != null)
                    Destroy(currentPlatform.placedHero);

                Vector3 spawnPos = currentPlatform.transform.position;
                GameObject hero = Instantiate(heroPrefabs[index], spawnPos, Quaternion.identity);
                currentPlatform.placedHero = hero;

                Debug.Log($"🧩 Đã đặt tướng {heroPrefabs[index].name} tại {currentPlatform.name}");
            }

            gameObject.SetActive(false);
            Time.timeScale = 1f;
            currentPlatform = null;
        }
        else
        {
            ShowNotEnoughGold();
        }
    }

    // 🔴 Xóa tướng và hoàn vàng (nếu muốn)
    private void RemoveHero()
    {
        if (currentPlatform != null && currentPlatform.placedHero != null)
        {
            Destroy(currentPlatform.placedHero);
            currentPlatform.placedHero = null;

            // ✅ Hoàn vàng lại một nửa (tuỳ chỉnh)
            if (GoldManager.Instance != null)
            {
                GoldManager.Instance.AddGold(100); // thay bằng công thức bạn muốn
            }

            Debug.Log("🗑️ Đã xóa tướng khỏi platform " + currentPlatform.name);
        }

        gameObject.SetActive(false);
        Time.timeScale = 1f;
        currentPlatform = null;
    }

    private void ShowNotEnoughGold()
    {
        if (notEnoughGoldText == null) return;
        StopAllCoroutines();
        StartCoroutine(ShowGoldWarning());
    }

    private System.Collections.IEnumerator ShowGoldWarning()
    {
        notEnoughGoldText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        notEnoughGoldText.gameObject.SetActive(false);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        if (removePanel != null)
            removePanel.SetActive(false);
        currentPlatform = null;
        Time.timeScale = 1f;

        if (notEnoughGoldText != null)
            notEnoughGoldText.gameObject.SetActive(false);
    }
}
