using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;

/// <summary>
/// Auto-fix cho GameSpeedController ở Map 2
/// Tự động tìm và assign các nút tốc độ
/// </summary>
public class FixMap2SpeedController : EditorWindow
{
    [MenuItem("Tools/Fix Map 2 Speed Controller 🔧")]
    public static void ShowWindow()
    {
        if (EditorUtility.DisplayDialog(
            "Fix Map 2 Speed Controller",
            "Công cụ này sẽ tự động:\n\n" +
            "1. Mở scene Game_Map2\n" +
            "2. Tìm GameSpeedController\n" +
            "3. Tìm và assign các nút tốc độ (0.5x, 1x, 2x)\n" +
            "4. Save scene\n\n" +
            "Bắt đầu fix?",
            "YES, FIX IT!",
            "Cancel"))
        {
            FixSpeedController();
        }
    }

    private static void FixSpeedController()
    {
        // 1. Mở scene Map 2
        var scenePath = "Assets/Scene/Game_Map2.unity";
        var scene = EditorSceneManager.OpenScene(scenePath);
        
        Debug.Log("[FixMap2] Opened scene: " + scene.name);

        // 2. Tìm GameSpeedController
        GameSpeedController controller = GameObject.FindObjectOfType<GameSpeedController>();
        
        if (controller == null)
        {
            EditorUtility.DisplayDialog(
                "Error",
                "Không tìm thấy GameSpeedController trong scene Map 2!\n\n" +
                "Vui lòng kiểm tra xem GameSpeedController có tồn tại trong scene không.",
                "OK");
            return;
        }

        Debug.Log("[FixMap2] Found GameSpeedController on: " + controller.gameObject.name);

        // 3. Tìm các nút tốc độ
        Button speed05xButton = FindButtonByName("0.5x", "05x", "Speed05x", "SpeedHalf");
        Button speed1xButton = FindButtonByName("1x", "Normal", "Speed1x", "SpeedNormal");
        Button speed2xButton = FindButtonByName("2x", "Fast", "Speed2x", "SpeedDouble", "SpeedFast");

        // 4. Assign vào controller
        bool anyAssigned = false;
        SerializedObject so = new SerializedObject(controller);

        if (speed05xButton != null)
        {
            so.FindProperty("speed05xButton").objectReferenceValue = speed05xButton;
            Debug.Log("[FixMap2] ✅ Assigned 0.5x button: " + speed05xButton.gameObject.name);
            anyAssigned = true;
        }
        else
        {
            Debug.LogWarning("[FixMap2] ⚠️ Could not find 0.5x speed button!");
        }

        if (speed1xButton != null)
        {
            so.FindProperty("speed1xButton").objectReferenceValue = speed1xButton;
            Debug.Log("[FixMap2] ✅ Assigned 1x button: " + speed1xButton.gameObject.name);
            anyAssigned = true;
        }
        else
        {
            Debug.LogWarning("[FixMap2] ⚠️ Could not find 1x speed button!");
        }

        if (speed2xButton != null)
        {
            so.FindProperty("speed2xButton").objectReferenceValue = speed2xButton;
            Debug.Log("[FixMap2] ✅ Assigned 2x button: " + speed2xButton.gameObject.name);
            anyAssigned = true;
        }
        else
        {
            Debug.LogWarning("[FixMap2] ⚠️ Could not find 2x speed button!");
        }

        so.ApplyModifiedProperties();

        // 5. Mark scene dirty và save
        if (anyAssigned)
        {
            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);

            EditorUtility.DisplayDialog(
                "Success! 🎉",
                $"Đã fix GameSpeedController!\n\n" +
                $"Buttons assigned:\n" +
                $"• 0.5x: {(speed05xButton != null ? "✅" : "❌")}\n" +
                $"• 1x: {(speed1xButton != null ? "✅" : "❌")}\n" +
                $"• 2x: {(speed2xButton != null ? "✅" : "❌")}\n\n" +
                "Scene đã được save.\n" +
                "Bây giờ bạn có thể test Map 2!",
                "Awesome!");
        }
        else
        {
            EditorUtility.DisplayDialog(
                "Warning",
                "Không tìm thấy button nào!\n\n" +
                "Có thể:\n" +
                "1. Buttons chưa được tạo trong scene\n" +
                "2. Tên buttons không khớp với pattern tìm kiếm\n\n" +
                "Vui lòng kiểm tra lại UI trong scene Map 2.",
                "OK");
        }
    }

    /// <summary>
    /// Tìm button theo nhiều pattern khác nhau
    /// </summary>
    private static Button FindButtonByName(params string[] namePatterns)
    {
        Button[] allButtons = GameObject.FindObjectsOfType<Button>(true);
        
        foreach (Button btn in allButtons)
        {
            string objName = btn.gameObject.name.ToLower();
            
            foreach (string pattern in namePatterns)
            {
                if (objName.Contains(pattern.ToLower()))
                {
                    return btn;
                }
            }
            
            // Kiểm tra cả text của button
            var textComponent = btn.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (textComponent != null)
            {
                string btnText = textComponent.text.ToLower();
                foreach (string pattern in namePatterns)
                {
                    if (btnText.Contains(pattern.ToLower()))
                    {
                        return btn;
                    }
                }
            }
            
            // Kiểm tra legacy Text component
            var legacyText = btn.GetComponentInChildren<UnityEngine.UI.Text>();
            if (legacyText != null)
            {
                string btnText = legacyText.text.ToLower();
                foreach (string pattern in namePatterns)
                {
                    if (btnText.Contains(pattern.ToLower()))
                    {
                        return btn;
                    }
                }
            }
        }
        
        return null;
    }
}
