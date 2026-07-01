using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

/// <summary>
/// Editor Script để tự động cập nhật tất cả Hero ScriptableObjects
/// Menu: Tools/Map 3/Update All Hero Data
/// </summary>
public class HeroDataUpdater : EditorWindow
{
    [MenuItem("Tools/Map 3/Update All Hero Data (Add Upgrade Stats)")]
    public static void UpdateAllHeroData()
    {
        // Tìm tất cả HeroData_Lv3 assets
        string[] guids = AssetDatabase.FindAssets("t:HeroData_Lv3", new[] { "Assets/Asset_map_3/ScriptableObjects/Hero" });
        
        if (guids.Length == 0)
        {
            Debug.LogWarning("Không tìm thấy HeroData_Lv3 nào!");
            EditorUtility.DisplayDialog("Warning", "Không tìm thấy Hero ScriptableObject nào!", "OK");
            return;
        }

        int updatedCount = 0;
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            HeroData_Lv3 heroData = AssetDatabase.LoadAssetAtPath<HeroData_Lv3>(path);
            
            if (heroData != null)
            {
                UpdateHeroData(heroData, path);
                updatedCount++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"✅ Đã cập nhật {updatedCount} Hero Data!");
        EditorUtility.DisplayDialog("Success", 
            $"✅ Đã cập nhật {updatedCount} Hero ScriptableObjects!\n\n" +
            "Các giá trị đã set:\n" +
            "- Max Level: 3\n" +
            "- Damage Multiplier: 0.2\n" +
            "- Range Multiplier: 0.1\n" +
            "- Shoot Speed Multiplier: 0.15", "OK");
    }

    private static void UpdateHeroData(HeroData_Lv3 heroData, string path)
    {
        SerializedObject so = new SerializedObject(heroData);

        // Set Max Level
        SerializedProperty maxLevel = so.FindProperty("maxLevel");
        if (maxLevel != null)
        {
            maxLevel.intValue = 3;
        }

        // Set Damage Upgrade Multiplier
        SerializedProperty damageMultiplier = so.FindProperty("damageUpgradeMultiplier");
        if (damageMultiplier != null)
        {
            // Đặt giá trị khác nhau tùy hero để đa dạng
            if (heroData.name.Contains("Tethys"))
                damageMultiplier.floatValue = 0.25f; // Buff mạnh hơn
            else if (heroData.name.Contains("Lorian"))
                damageMultiplier.floatValue = 0.20f;
            else
                damageMultiplier.floatValue = 0.20f;
        }

        // Set Range Upgrade Multiplier
        SerializedProperty rangeMultiplier = so.FindProperty("rangeUpgradeMultiplier");
        if (rangeMultiplier != null)
        {
            if (heroData.name.Contains("Tethys"))
                rangeMultiplier.floatValue = 0.12f; // Buff mạnh hơn
            else if (heroData.name.Contains("Lorian"))
                rangeMultiplier.floatValue = 0.10f;
            else
                rangeMultiplier.floatValue = 0.10f;
        }

        // Set Shoot Speed Upgrade Multiplier
        SerializedProperty shootSpeedMultiplier = so.FindProperty("shootSpeedUpgradeMultiplier");
        if (shootSpeedMultiplier != null)
        {
            if (heroData.name.Contains("Lorian"))
                shootSpeedMultiplier.floatValue = 0.20f; // Bắn nhanh hơn
            else if (heroData.name.Contains("Tethys"))
                shootSpeedMultiplier.floatValue = 0.18f;
            else
                shootSpeedMultiplier.floatValue = 0.15f;
        }

        so.ApplyModifiedProperties();
        EditorUtility.SetDirty(heroData);

        Debug.Log($"✅ Đã cập nhật: {heroData.name}");
    }

    [MenuItem("Tools/Map 3/Show Hero Stats Preview")]
    public static void ShowHeroStatsPreview()
    {
        string[] guids = AssetDatabase.FindAssets("t:HeroData_Lv3", new[] { "Assets/Asset_map_3/ScriptableObjects/Hero" });
        
        if (guids.Length == 0)
        {
            Debug.LogWarning("Không tìm thấy HeroData_Lv3 nào!");
            return;
        }

        string preview = "📊 HERO STATS PREVIEW - ALL LEVELS\n\n";

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            HeroData_Lv3 data = AssetDatabase.LoadAssetAtPath<HeroData_Lv3>(path);
            
            if (data != null)
            {
                preview += $"🎯 {data.name}\n";
                preview += $"   Cost: {data.cost} gold | Upgrade Cost: {data.upgradeCost}/level\n";
                preview += $"   Base: {data.damage} dmg | {data.range} range | {(1f/data.shootInterval):F2} atk/s\n";
                
                // Calculate Level 2
                float dmg2 = data.damage * (1 + data.damageUpgradeMultiplier);
                float rng2 = data.range * (1 + data.rangeUpgradeMultiplier);
                float spd2 = 1f / (data.shootInterval * (1 - data.shootSpeedUpgradeMultiplier));
                preview += $"   Lv2: {dmg2:F1} dmg | {rng2:F1} range | {spd2:F2} atk/s\n";
                
                // Calculate Level 3
                float dmg3 = data.damage * (1 + data.damageUpgradeMultiplier * 2);
                float rng3 = data.range * (1 + data.rangeUpgradeMultiplier * 2);
                float spd3 = 1f / (data.shootInterval * (1 - data.shootSpeedUpgradeMultiplier * 2));
                preview += $"   Lv3: {dmg3:F1} dmg | {rng3:F1} range | {spd3:F2} atk/s\n";
                
                // Ice hero bonus
                if (data.isMapIceHero)
                {
                    float dmgIce = dmg3 * data.iceDamageMultiplier;
                    float spdIce = spd3 * data.iceAttackSpeedMultiplier;
                    preview += $"   💎 Vs Ice: {dmgIce:F1} dmg | {spdIce:F2} atk/s | DPS: {(dmgIce * spdIce):F1}\n";
                }
                
                preview += "\n";
            }
        }

        Debug.Log(preview);
        
        EditorUtility.DisplayDialog("Hero Stats Preview", 
            "Xem Console để xem chi tiết stats của tất cả hero!", "OK");
    }
}
#endif
