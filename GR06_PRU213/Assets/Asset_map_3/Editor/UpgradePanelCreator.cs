using UnityEngine;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;

/// <summary>
/// Editor Script để tự động tạo Upgrade Panel UI
/// Menu: Tools/Map 3/Create Upgrade Panel
/// </summary>
public class UpgradePanelCreator : EditorWindow
{
    [MenuItem("Tools/Map 3/Create Upgrade Panel UI")]
    public static void CreateUpgradePanelUI()
    {
        // Tìm Canvas trong Scene
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Không tìm thấy Canvas trong Scene! Hãy tạo Canvas trước.");
            EditorUtility.DisplayDialog("Error", "Không tìm thấy Canvas trong Scene!", "OK");
            return;
        }

        // Kiểm tra đã có panel chưa
        HeroUpgradePanel_Lv3 existingPanel = FindObjectOfType<HeroUpgradePanel_Lv3>();
        if (existingPanel != null)
        {
            bool overwrite = EditorUtility.DisplayDialog(
                "Panel Already Exists",
                "HeroUpgradePanel đã tồn tại! Có muốn tạo lại không?",
                "Yes, Overwrite",
                "Cancel"
            );
            
            if (!overwrite) return;
            DestroyImmediate(existingPanel.gameObject);
        }

        // Bắt đầu tạo UI
        GameObject panel = CreatePanel(canvas);
        CreateUIElements(panel);
        
        // Gán script và references
        SetupPanelScript(panel);

        Debug.Log("✅ Đã tạo HeroUpgradePanel thành công!");
        EditorUtility.DisplayDialog("Success", 
            "✅ Đã tạo HeroUpgradePanel!\n\nHãy kiểm tra trong Canvas.", "OK");
        
        Selection.activeGameObject = panel;
    }

    private static GameObject CreatePanel(Canvas canvas)
    {
        // Tạo Panel chính
        GameObject panel = new GameObject("HeroUpgradePanel");
        panel.transform.SetParent(canvas.transform, false);

        // Add RectTransform
        RectTransform rect = panel.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.sizeDelta = new Vector2(400, 500);

        // Add Image (background)
        Image bg = panel.AddComponent<Image>();
        bg.color = new Color(0.1f, 0.1f, 0.1f, 0.95f);

        // Ẩn panel ban đầu
        panel.SetActive(false);

        return panel;
    }

    private static void CreateUIElements(GameObject panel)
    {
        // 1. Hero Name Text
        CreateTextMeshPro(panel, "TxtHeroName", 
            new Vector2(0, 200), new Vector2(350, 50), 28, 
            "Hero Name", TextAlignmentOptions.Center, Color.white);

        // 2. Level Text
        CreateTextMeshPro(panel, "TxtLevel", 
            new Vector2(0, 150), new Vector2(200, 40), 22, 
            "Level 1/3", TextAlignmentOptions.Center, Color.yellow);

        // 3. Current Stats Text
        CreateTextMeshPro(panel, "TxtCurrentStats", 
            new Vector2(-10, 70), new Vector2(360, 100), 16, 
            "<b>Current Stats:</b>\nDamage: 35\nRange: 3.0\nAttack Speed: 1.0/s", 
            TextAlignmentOptions.TopLeft, Color.white);

        // 4. Next Stats Text
        CreateTextMeshPro(panel, "TxtNextStats", 
            new Vector2(-10, -40), new Vector2(360, 100), 16, 
            "<b>Next Level:</b>\nDamage: 42 <color=green>(+7)</color>\nRange: 3.3 <color=green>(+0.3)</color>\nAttack Speed: 1.18/s <color=green>(+0.18)</color>", 
            TextAlignmentOptions.TopLeft, new Color(0.8f, 1f, 0.8f));

        // 5. Upgrade Button
        GameObject btnUpgrade = CreateButton(panel, "BtnUpgrade", 
            new Vector2(-90, -170), new Vector2(180, 50), 
            "Upgrade (50 Gold)", new Color(0.2f, 0.8f, 0.2f));

        // 6. Sell Button
        GameObject btnSell = CreateButton(panel, "BtnSell", 
            new Vector2(90, -170), new Vector2(180, 50), 
            "Sell (70 Gold)", new Color(0.8f, 0.2f, 0.2f));

        // 7. Close Button
        GameObject btnClose = CreateButton(panel, "BtnClose", 
            new Vector2(170, 210), new Vector2(40, 40), 
            "X", new Color(0.5f, 0.5f, 0.5f));
    }

    private static GameObject CreateTextMeshPro(GameObject parent, string name, 
        Vector2 position, Vector2 size, int fontSize, string text, 
        TextAlignmentOptions alignment, Color color)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(parent.transform, false);

        RectTransform rect = obj.AddComponent<RectTransform>();
        rect.anchoredPosition = position;
        rect.sizeDelta = size;

        TextMeshProUGUI tmp = obj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = fontSize;
        tmp.alignment = alignment;
        tmp.color = color;
        tmp.enableWordWrapping = true;

        return obj;
    }

    private static GameObject CreateButton(GameObject parent, string name, 
        Vector2 position, Vector2 size, string text, Color color)
    {
        GameObject btnObj = new GameObject(name);
        btnObj.transform.SetParent(parent.transform, false);

        RectTransform rect = btnObj.AddComponent<RectTransform>();
        rect.anchoredPosition = position;
        rect.sizeDelta = size;

        Image img = btnObj.AddComponent<Image>();
        img.color = color;

        Button btn = btnObj.AddComponent<Button>();
        
        // Tạo Text cho button
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(btnObj.transform, false);

        RectTransform textRect = textObj.AddComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.sizeDelta = Vector2.zero;

        TextMeshProUGUI tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = name == "BtnClose" ? 24 : 18;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.white;

        return btnObj;
    }

    private static void SetupPanelScript(GameObject panel)
    {
        HeroUpgradePanel_Lv3 script = panel.AddComponent<HeroUpgradePanel_Lv3>();

        // Dùng SerializedObject để gán references
        SerializedObject so = new SerializedObject(script);

        // Gán panel object
        so.FindProperty("panelObject").objectReferenceValue = panel;

        // Gán các text fields
        so.FindProperty("heroNameText").objectReferenceValue = 
            panel.transform.Find("TxtHeroName").GetComponent<TextMeshProUGUI>();
        so.FindProperty("levelText").objectReferenceValue = 
            panel.transform.Find("TxtLevel").GetComponent<TextMeshProUGUI>();
        so.FindProperty("currentStatsText").objectReferenceValue = 
            panel.transform.Find("TxtCurrentStats").GetComponent<TextMeshProUGUI>();
        so.FindProperty("nextStatsText").objectReferenceValue = 
            panel.transform.Find("TxtNextStats").GetComponent<TextMeshProUGUI>();

        // Gán buttons
        so.FindProperty("upgradeButton").objectReferenceValue = 
            panel.transform.Find("BtnUpgrade").GetComponent<Button>();
        so.FindProperty("sellButton").objectReferenceValue = 
            panel.transform.Find("BtnSell").GetComponent<Button>();
        so.FindProperty("closeButton").objectReferenceValue = 
            panel.transform.Find("BtnClose").GetComponent<Button>();

        // Gán cost texts
        so.FindProperty("upgradeCostText").objectReferenceValue = 
            panel.transform.Find("BtnUpgrade/Text").GetComponent<TextMeshProUGUI>();
        so.FindProperty("sellValueText").objectReferenceValue = 
            panel.transform.Find("BtnSell/Text").GetComponent<TextMeshProUGUI>();

        so.ApplyModifiedProperties();

        Debug.Log("✅ Đã gán tất cả references tự động!");
    }
}
#endif
