using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

/// <summary>
/// Editor Script để tự động gán UpgradePanel cho tất cả Platforms
/// Menu: Tools/Map 3/Assign Panel to All Platforms
/// </summary>
public class PlatformSetup : EditorWindow
{
    [MenuItem("Tools/Map 3/Assign Panel to All Platforms")]
    public static void AssignPanelToAllPlatforms()
    {
        // Tìm UpgradePanel (bao gồm cả inactive objects)
        HeroUpgradePanel_Lv3 upgradePanel = Object.FindObjectOfType<HeroUpgradePanel_Lv3>(true);
        
        if (upgradePanel == null)
        {
            Debug.LogError("❌ Không tìm thấy HeroUpgradePanel_Lv3 trong Scene!");
            EditorUtility.DisplayDialog("Error", 
                "Không tìm thấy HeroUpgradePanel_Lv3!\n\n" +
                "Hãy tạo panel trước bằng:\n" +
                "Tools → Map 3 → Create Upgrade Panel UI", "OK");
            return;
        }

        // Tìm tất cả Platforms
        Platform_Lv3[] platforms = Object.FindObjectsOfType<Platform_Lv3>();
        
        if (platforms.Length == 0)
        {
            Debug.LogWarning("⚠️ Không tìm thấy Platform_Lv3 nào trong Scene!");
            EditorUtility.DisplayDialog("Warning", 
                "Không tìm thấy Platform_Lv3 nào trong Scene!", "OK");
            return;
        }

        int assignedCount = 0;
        foreach (Platform_Lv3 platform in platforms)
        {
            SerializedObject so = new SerializedObject(platform);
            SerializedProperty upgradePanelProp = so.FindProperty("upgradePanel");
            
            if (upgradePanelProp != null)
            {
                upgradePanelProp.objectReferenceValue = upgradePanel;
                so.ApplyModifiedProperties();
                EditorUtility.SetDirty(platform);
                assignedCount++;
            }
        }

        Debug.Log($"✅ Đã gán UpgradePanel cho {assignedCount} Platform(s)!");
        EditorUtility.DisplayDialog("Success", 
            $"✅ Đã gán UpgradePanel cho {assignedCount} Platforms!", "OK");
    }

    [MenuItem("Tools/Map 3/Complete Setup (All-in-One)")]
    public static void CompleteSetup()
    {
        bool confirm = EditorUtility.DisplayDialog(
            "Complete Setup",
            "Thao tác này sẽ:\n\n" +
            "1. Cập nhật tất cả Hero ScriptableObjects\n" +
            "2. Tạo Upgrade Panel UI (nếu chưa có)\n" +
            "3. Gán Panel cho tất cả Platforms\n\n" +
            "Tiếp tục?",
            "Yes, Do It!",
            "Cancel"
        );

        if (!confirm) return;

        // Bước 1: Update Hero Data
        Debug.Log("📝 Bước 1: Cập nhật Hero Data...");
        HeroDataUpdater.UpdateAllHeroData();

        // Bước 2: Create Panel
        Debug.Log("🎨 Bước 2: Tạo Upgrade Panel...");
        UpgradePanelCreator.CreateUpgradePanelUI();

        // Đợi một chút cho Unity update
        EditorApplication.delayCall += () =>
        {
            // Bước 3: Assign to Platforms
            Debug.Log("🔗 Bước 3: Gán Panel cho Platforms...");
            AssignPanelToAllPlatforms();

            // Hiển thị preview
            Debug.Log("📊 Bước 4: Hiển thị Stats Preview...");
            HeroDataUpdater.ShowHeroStatsPreview();

            Debug.Log("🎉 HOÀN THÀNH TẤT CẢ!");
            EditorUtility.DisplayDialog("Complete!", 
                "🎉 ĐÃ SETUP XONG HỆ THỐNG NÂNG CẤP!\n\n" +
                "✅ Hero Data: Updated\n" +
                "✅ Upgrade Panel: Created\n" +
                "✅ Platforms: Connected\n\n" +
                "Kiểm tra Console để xem stats preview!", "Awesome!");
        };
    }

    [MenuItem("Tools/Map 3/Verify Setup")]
    public static void VerifySetup()
    {
        string report = "🔍 VERIFICATION REPORT\n\n";
        bool allGood = true;

        // Check 1: Hero Data
        string[] guids = AssetDatabase.FindAssets("t:HeroData_Lv3", new[] { "Assets/Asset_map_3/ScriptableObjects/Hero" });
        report += $"✅ Found {guids.Length} Hero ScriptableObjects\n";
        
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            HeroData_Lv3 data = AssetDatabase.LoadAssetAtPath<HeroData_Lv3>(path);
            
            if (data.maxLevel != 3)
            {
                report += $"   ⚠️ {data.name}: maxLevel = {data.maxLevel} (should be 3)\n";
                allGood = false;
            }
        }

        // Check 2: Upgrade Panel
        HeroUpgradePanel_Lv3 panel = Object.FindObjectOfType<HeroUpgradePanel_Lv3>(true);
        if (panel == null)
        {
            report += "❌ HeroUpgradePanel_Lv3 NOT FOUND in Scene!\n";
            allGood = false;
        }
        else
        {
            report += "✅ HeroUpgradePanel_Lv3 found\n";
        }

        // Check 3: Platforms
        Platform_Lv3[] platforms = Object.FindObjectsOfType<Platform_Lv3>();
        report += $"✅ Found {platforms.Length} Platforms\n";
        
        int connectedCount = 0;
        foreach (Platform_Lv3 platform in platforms)
        {
            SerializedObject so = new SerializedObject(platform);
            SerializedProperty prop = so.FindProperty("upgradePanel");
            
            if (prop != null && prop.objectReferenceValue != null)
            {
                connectedCount++;
            }
        }
        
        report += $"   {connectedCount}/{platforms.Length} Platforms connected to Panel\n";
        if (connectedCount < platforms.Length)
        {
            report += $"   ⚠️ {platforms.Length - connectedCount} Platform(s) NOT connected!\n";
            allGood = false;
        }

        // Check 4: Scripts
        report += "\n📄 Scripts Status:\n";
        report += CheckScriptExists("Hero_Lv3") ? "✅ Hero_Lv3.cs\n" : "❌ Hero_Lv3.cs NOT FOUND\n";
        report += CheckScriptExists("HeroUpgradePanel_Lv3") ? "✅ HeroUpgradePanel_Lv3.cs\n" : "❌ HeroUpgradePanel_Lv3.cs NOT FOUND\n";
        report += CheckScriptExists("Platform_Lv3") ? "✅ Platform_Lv3.cs\n" : "❌ Platform_Lv3.cs NOT FOUND\n";

        report += "\n" + (allGood ? "🎉 EVERYTHING LOOKS GOOD!" : "⚠️ SOME ISSUES FOUND!");

        Debug.Log(report);
        EditorUtility.DisplayDialog(allGood ? "All Good!" : "Issues Found", 
            report, "OK");
    }

    private static bool CheckScriptExists(string scriptName)
    {
        string[] guids = AssetDatabase.FindAssets(scriptName + " t:Script");
        return guids.Length > 0;
    }
}
#endif
