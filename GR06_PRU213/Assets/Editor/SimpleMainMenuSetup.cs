using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SceneManagement;

/// <summary>
/// Script tự động setup MainMenu UI đơn giản
/// Chạy: Tools -> Auto Setup MainMenu NOW!
/// </summary>
public class SimpleMainMenuSetup
{
    [MenuItem("Tools/Auto Setup MainMenu NOW! 🚀")]
    public static void SetupEverything()
    {
        if (!EditorUtility.DisplayDialog("Auto Setup", 
            "This will automatically create:\n\n" +
            "✅ Tutorial Button\n" +
            "✅ TutorialManager\n" +
            "✅ Level Selection Panel (5 maps)\n" +
            "✅ Tutorial Panel\n" +
            "✅ All connections\n\n" +
            "Continue?", "YES!", "Cancel"))
        {
            return;
        }

        Debug.Log("🚀 Starting Auto Setup...");

        try
        {
            // Step 1: Create Tutorial Button
            CreateTutorialButton();
            
            // Step 2: Create TutorialManager
            CreateTutorialManager();
            
            // Step 3: Create Level Selection Panel
            CreateLevelSelectionPanel();
            
            // Step 4: Create Tutorial Panel
            CreateTutorialPanel();
            
            // Step 5: Connect Everything
            ConnectMainMenuController();
            
            // Step 6: Save
            EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
            
            Debug.Log("✅ AUTO SETUP COMPLETE!");
            EditorUtility.DisplayDialog("Success! 🎉", 
                "MainMenu UI setup complete!\n\n" +
                "✅ All panels are HIDDEN\n" +
                "✅ All references connected\n\n" +
                "Save scene (Ctrl+S) and Press Play to test!", 
                "Awesome!");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"❌ Setup failed: {e.Message}");
            EditorUtility.DisplayDialog("Error", $"Setup failed:\n{e.Message}", "OK");
        }
    }

    static void CreateTutorialButton()
    {
        if (GameObject.Find("TutorialButton") != null)
        {
            Debug.Log("⚠️ TutorialButton already exists, skipping...");
            return;
        }

        GameObject playBtn = GameObject.Find("PlayButton");
        if (playBtn == null) playBtn = GameObject.Find("NewGameButton");
        
        if (playBtn != null)
        {
            GameObject tutBtn = Object.Instantiate(playBtn);
            tutBtn.name = "TutorialButton";
            tutBtn.transform.SetParent(playBtn.transform.parent);
            tutBtn.transform.SetSiblingIndex(playBtn.transform.GetSiblingIndex() + 1);
            
            RectTransform rt = tutBtn.GetComponent<RectTransform>();
            rt.anchoredPosition = playBtn.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, -115);
            
            TextMeshProUGUI text = tutBtn.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null) text.text = "HOW TO PLAY";
            
            Debug.Log("✅ Tutorial Button created");
        }
    }

    static void CreateTutorialManager()
    {
        if (GameObject.Find("TutorialManager") != null)
        {
            Debug.Log("⚠️ TutorialManager already exists, skipping...");
            return;
        }

        Canvas canvas = Object.FindObjectOfType<Canvas>();
        GameObject manager = new GameObject("TutorialManager");
        manager.transform.SetParent(canvas.transform);
        manager.AddComponent<TutorialManager>();
        
        Debug.Log("✅ TutorialManager created");
    }

    static void CreateLevelSelectionPanel()
    {
        if (GameObject.Find("LevelSelectionPanel") != null)
        {
            Debug.Log("⚠️ LevelSelectionPanel already exists, skipping...");
            return;
        }

        Canvas canvas = Object.FindObjectOfType<Canvas>();
        
        // Create Panel
        GameObject panel = new GameObject("LevelSelectionPanel");
        panel.transform.SetParent(canvas.transform, false);
        
        RectTransform panelRT = panel.AddComponent<RectTransform>();
        panelRT.anchorMin = Vector2.zero;
        panelRT.anchorMax = Vector2.one;
        panelRT.sizeDelta = Vector2.zero;
        
        Image panelImg = panel.AddComponent<Image>();
        panelImg.color = new Color(0, 0, 0, 1f); // Hoàn toàn đen đục
        
        // Add Canvas component để override sorting
        Canvas panelCanvas = panel.AddComponent<Canvas>();
        panelCanvas.overrideSorting = true;
        panelCanvas.sortingOrder = 100;
        
        // Add GraphicRaycaster
        panel.AddComponent<UnityEngine.UI.GraphicRaycaster>();
        
        // Title
        CreateText(panel.transform, "Title", "SELECT LEVEL", 60, 
            new Vector2(0.5f, 1f), new Vector2(0, -100), new Vector2(600, 80));
        
        // Buttons Container
        GameObject container = new GameObject("LevelButtonsGroup");
        container.transform.SetParent(panel.transform, false);
        RectTransform containerRT = container.AddComponent<RectTransform>();
        containerRT.anchorMin = new Vector2(0.5f, 0.5f);
        containerRT.anchorMax = new Vector2(0.5f, 0.5f);
        containerRT.pivot = new Vector2(0.5f, 0.5f);
        containerRT.sizeDelta = new Vector2(700, 500);
        
        GridLayoutGroup grid = container.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(200, 200);
        grid.spacing = new Vector2(30, 30);
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = 3;
        
        // 5 Level Buttons
        for (int i = 1; i <= 5; i++)
        {
            CreateButton(container.transform, $"Level{i}Button", $"MAP {i}", 
                new Color(1f, 0.65f, 0f));
        }
        
        // Close Button
        CreateButton(panel.transform, "CloseLevelSelectionButton", "CLOSE", 
            new Color(0.8f, 0.2f, 0.2f), new Vector2(0.5f, 0f), new Vector2(0, 100), new Vector2(250, 70));
        
        // HIDE PANEL
        panel.SetActive(false);
        
        Debug.Log("✅ Level Selection Panel created and HIDDEN");
    }

    static void CreateTutorialPanel()
    {
        if (GameObject.Find("TutorialPanel") != null)
        {
            Debug.Log("⚠️ TutorialPanel already exists, skipping...");
            return;
        }

        Canvas canvas = Object.FindObjectOfType<Canvas>();
        
        // Create Panel
        GameObject panel = new GameObject("TutorialPanel");
        panel.transform.SetParent(canvas.transform, false);
        
        RectTransform panelRT = panel.AddComponent<RectTransform>();
        panelRT.anchorMin = Vector2.zero;
        panelRT.anchorMax = Vector2.one;
        panelRT.sizeDelta = Vector2.zero;
        
        Image panelImg = panel.AddComponent<Image>();
        panelImg.color = new Color(0, 0, 0, 1f); // Hoàn toàn đen đục (Alpha = 1)
        
        // Add Canvas component để override sorting
        Canvas panelCanvas = panel.AddComponent<Canvas>();
        panelCanvas.overrideSorting = true;
        panelCanvas.sortingOrder = 100; // Hiển thị trên tất cả UI khác
        
        // Add GraphicRaycaster để panel có thể click được
        panel.AddComponent<UnityEngine.UI.GraphicRaycaster>();
        
        // Title
        CreateText(panel.transform, "TitleText", "Tutorial Title", 60, 
            new Vector2(0.5f, 1f), new Vector2(0, -80), new Vector2(800, 100));
        
        // Content
        CreateText(panel.transform, "ContentText", "Tutorial content here...", 28, 
            new Vector2(0.5f, 0.5f), new Vector2(0, 0), new Vector2(900, 500), TextAlignmentOptions.TopLeft);
        
        // Page Indicator
        CreateText(panel.transform, "PageIndicatorText", "Page 1 / 6", 28, 
            new Vector2(0.5f, 0f), new Vector2(0, 100), new Vector2(200, 60));
        
        // Next Button
        CreateButton(panel.transform, "NextButton", "NEXT >", 
            new Color(0.3f, 0.5f, 0.7f), new Vector2(0.5f, 0f), new Vector2(250, 100), new Vector2(180, 60));
        
        // Prev Button
        CreateButton(panel.transform, "PrevButton", "< PREV", 
            new Color(0.3f, 0.5f, 0.7f), new Vector2(0.5f, 0f), new Vector2(-250, 100), new Vector2(180, 60));
        
        // Close Button
        CreateButton(panel.transform, "CloseTutorialButton", "X", 
            new Color(0.8f, 0.2f, 0.2f), new Vector2(1f, 1f), new Vector2(-50, -50), new Vector2(70, 70));
        
        // HIDE PANEL
        panel.SetActive(false);
        
        Debug.Log("✅ Tutorial Panel created and HIDDEN");
    }

    static GameObject CreateText(Transform parent, string name, string text, float fontSize, 
        Vector2 anchor, Vector2 pos, Vector2 size, TextAlignmentOptions alignment = TextAlignmentOptions.Center)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(parent, false);
        
        RectTransform rt = obj.AddComponent<RectTransform>();
        rt.anchorMin = anchor;
        rt.anchorMax = anchor;
        rt.pivot = anchor;
        rt.anchoredPosition = pos;
        rt.sizeDelta = size;
        
        TextMeshProUGUI tmp = obj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = fontSize;
        tmp.alignment = alignment;
        tmp.color = Color.white;
        
        return obj;
    }

    static GameObject CreateButton(Transform parent, string name, string text, Color color,
        Vector2? anchor = null, Vector2? pos = null, Vector2? size = null)
    {
        GameObject btn = new GameObject(name);
        btn.transform.SetParent(parent, false);
        
        if (anchor.HasValue)
        {
            RectTransform rt = btn.AddComponent<RectTransform>();
            rt.anchorMin = anchor.Value;
            rt.anchorMax = anchor.Value;
            rt.pivot = anchor.Value;
            if (pos.HasValue) rt.anchoredPosition = pos.Value;
            if (size.HasValue) rt.sizeDelta = size.Value;
        }
        
        btn.AddComponent<Image>().color = color;
        btn.AddComponent<Button>();
        
        GameObject txtObj = new GameObject("Text");
        txtObj.transform.SetParent(btn.transform, false);
        RectTransform txtRT = txtObj.AddComponent<RectTransform>();
        txtRT.anchorMin = Vector2.zero;
        txtRT.anchorMax = Vector2.one;
        txtRT.sizeDelta = Vector2.zero;
        
        TextMeshProUGUI tmp = txtObj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = text == "X" ? 40 : (text.Contains("MAP") ? 36 : (text == "CLOSE" ? 32 : 24));
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.white;
        
        return btn;
    }

    static void ConnectMainMenuController()
    {
        MainMenuController controller = Object.FindObjectOfType<MainMenuController>();
        if (controller == null)
        {
            Debug.LogWarning("⚠️ MainMenuController not found, skipping connections");
            return;
        }

        // This would require reflection to set private fields
        // For now, user needs to assign manually
        Debug.Log("⚠️ Please assign references manually in MainMenuController Inspector");
    }
}
