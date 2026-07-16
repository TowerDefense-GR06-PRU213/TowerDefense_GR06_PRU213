using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using TMPro;

/// <summary>
/// Tự động setup GameSpeedToggle cho Map 2
/// Convert từ 3 buttons sang 1 button toggle
/// </summary>
public class SetupSpeedToggle : EditorWindow
{
    [MenuItem("Tools/Setup Map 2 Speed Toggle (1 Button) 🔄")]
    public static void ShowWindow()
    {
        if (EditorUtility.DisplayDialog(
            "Setup Speed Toggle",
            "Công cụ này sẽ:\n\n" +
            "1. Mở scene Game_Map2\n" +
            "2. Tìm hoặc tạo 1 nút tốc độ duy nhất\n" +
            "3. Add GameSpeedToggle component\n" +
            "4. Setup tự động\n" +
            "5. Xóa GameSpeedController cũ (nếu có)\n\n" +
            "Nút sẽ cycle: 0.5x → 1x → 2x → 0.5x...\n\n" +
            "Bắt đầu?",
            "YES, DO IT!",
            "Cancel"))
        {
            SetupToggle();
        }
    }

    private static void SetupToggle()
    {
        // 1. Mở scene Map 2
        var scenePath = "Assets/Scene/Game_Map2.unity";
        var scene = EditorSceneManager.OpenScene(scenePath);
        
        Debug.Log("[SetupSpeedToggle] Opened scene: " + scene.name);

        // 2. Tìm hoặc tạo button
        Button toggleButton = FindOrCreateSpeedButton();
        
        if (toggleButton == null)
        {
            EditorUtility.DisplayDialog(
                "Error",
                "Không thể tạo hoặc tìm button!\n\n" +
                "Vui lòng tạo 1 button thủ công trong Canvas.",
                "OK");
            return;
        }

        Debug.Log("[SetupSpeedToggle] Found/Created button: " + toggleButton.gameObject.name);

        // 3. Remove old GameSpeedController nếu có
        GameSpeedController oldController = GameObject.FindObjectOfType<GameSpeedController>();
        if (oldController != null)
        {
            Debug.Log("[SetupSpeedToggle] Removing old GameSpeedController...");
            GameObject.DestroyImmediate(oldController);
        }

        // 4. Add GameSpeedToggle component
        GameObject buttonObj = toggleButton.gameObject;
        GameSpeedToggle speedToggle = buttonObj.GetComponent<GameSpeedToggle>();
        
        if (speedToggle == null)
        {
            speedToggle = buttonObj.AddComponent<GameSpeedToggle>();
            Debug.Log("[SetupSpeedToggle] Added GameSpeedToggle component");
        }

        // 5. Setup references
        SerializedObject so = new SerializedObject(speedToggle);
        
        so.FindProperty("speedToggleButton").objectReferenceValue = toggleButton;
        
        // Tìm text component
        TextMeshProUGUI buttonText = toggleButton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            so.FindProperty("speedText").objectReferenceValue = buttonText;
            buttonText.text = "1.0x"; // Set text mặc định
        }
        
        // Set colors
        so.FindProperty("normalSpeedColor").colorValue = Color.white;
        so.FindProperty("slowSpeedColor").colorValue = new Color(1f, 0.92f, 0.016f); // Yellow
        so.FindProperty("fastSpeedColor").colorValue = new Color(0f, 1f, 0f); // Green
        
        so.ApplyModifiedProperties();

        // 6. Position button (nếu là button mới tạo)
        RectTransform rectTransform = toggleButton.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            // Đặt ở góc trên phải
            rectTransform.anchorMin = new Vector2(1, 1);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(1, 1);
            rectTransform.anchoredPosition = new Vector2(-20, -20);
            rectTransform.sizeDelta = new Vector2(100, 50);
        }

        // 7. Save scene
        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);

        EditorUtility.DisplayDialog(
            "Success! 🎉",
            $"Đã setup Speed Toggle thành công!\n\n" +
            $"Button: {toggleButton.gameObject.name}\n" +
            $"Component: GameSpeedToggle ✅\n\n" +
            "Cách dùng:\n" +
            "• Click button → Tốc độ thay đổi\n" +
            "• 0.5x (Vàng) → 1x (Trắng) → 2x (Xanh)\n" +
            "• Click tiếp → Lặp lại từ đầu\n\n" +
            "Test ngay Map 2!",
            "Awesome!");
    }

    /// <summary>
    /// Tìm button hiện có hoặc tạo mới
    /// </summary>
    private static Button FindOrCreateSpeedButton()
    {
        // Tìm button 0.5x đã có (từ lần fix trước)
        Button[] allButtons = GameObject.FindObjectsOfType<Button>(true);
        
        foreach (Button btn in allButtons)
        {
            string objName = btn.gameObject.name.ToLower();
            
            // Ưu tiên button 0.5x hoặc speed button
            if (objName.Contains("speed") || objName.Contains("05x") || objName.Contains("0.5"))
            {
                return btn;
            }
        }

        // Nếu không tìm thấy, tạo mới
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("[SetupSpeedToggle] Không tìm thấy Canvas!");
            return null;
        }

        // Tạo button mới
        GameObject buttonObj = new GameObject("SpeedToggleButton");
        buttonObj.transform.SetParent(canvas.transform, false);
        
        Button button = buttonObj.AddComponent<Button>();
        Image image = buttonObj.AddComponent<Image>();
        image.color = Color.white;
        
        // Add text
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(buttonObj.transform, false);
        
        TextMeshProUGUI text = textObj.AddComponent<TextMeshProUGUI>();
        text.text = "1.0x";
        text.fontSize = 24;
        text.color = Color.black;
        text.alignment = TextAlignmentOptions.Center;
        
        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.sizeDelta = Vector2.zero;
        textRect.anchoredPosition = Vector2.zero;
        
        Debug.Log("[SetupSpeedToggle] Created new button");
        
        return button;
    }
}
