using UnityEngine;

/// <summary>
/// Script helper để tự động gán UpgradePanel cho tất cả Platforms
/// Attach vào GameObject bất kỳ trong Scene (ví dụ: GameManager)
/// </summary>
public class UpgradePanelAutoAssigner_Lv3 : MonoBehaviour
{
    [Header("Auto Assign Settings")]
    [Tooltip("Có tự động gán khi Start không?")]
    [SerializeField] private bool autoAssignOnStart = true;

    [Tooltip("Panel sẽ được gán cho tất cả Platforms")]
    [SerializeField] private HeroUpgradePanel_Lv3 upgradePanel;

    private void Start()
    {
        if (autoAssignOnStart)
        {
            AssignPanelToAllPlatforms();
        }
    }

    /// <summary>
    /// Gán UpgradePanel cho tất cả Platform trong Scene
    /// Có thể gọi từ Inspector button hoặc code khác
    /// </summary>
    [ContextMenu("Assign Panel to All Platforms")]
    public void AssignPanelToAllPlatforms()
    {
        // Tìm panel nếu chưa gán
        if (upgradePanel == null)
        {
            upgradePanel = FindObjectOfType<HeroUpgradePanel_Lv3>();
            if (upgradePanel == null)
            {
                Debug.LogError("Không tìm thấy HeroUpgradePanel_Lv3 trong Scene! " +
                              "Hãy tạo panel trước.");
                return;
            }
        }

        // Tìm tất cả Platform
        Platform_Lv3[] platforms = FindObjectsOfType<Platform_Lv3>();
        
        if (platforms.Length == 0)
        {
            Debug.LogWarning("Không tìm thấy Platform_Lv3 nào trong Scene!");
            return;
        }

        int assignedCount = 0;
        foreach (Platform_Lv3 platform in platforms)
        {
            // Gán panel (cần làm field public tạm thời)
            SetUpgradePanel(platform, upgradePanel);
            assignedCount++;
        }

        Debug.Log($"✅ Đã gán UpgradePanel cho {assignedCount} Platform(s)!");
    }

    /// <summary>
    /// Set upgrade panel cho platform bằng reflection
    /// (vì field là private trong Platform_Lv3)
    /// </summary>
    private void SetUpgradePanel(Platform_Lv3 platform, HeroUpgradePanel_Lv3 panel)
    {
        // Dùng Reflection để set private field
        var field = typeof(Platform_Lv3).GetField("upgradePanel", 
            System.Reflection.BindingFlags.NonPublic | 
            System.Reflection.BindingFlags.Instance);
        
        if (field != null)
        {
            field.SetValue(platform, panel);
        }
        else
        {
            Debug.LogError("Không tìm thấy field 'upgradePanel' trong Platform_Lv3. " +
                          "Đảm bảo field tên đúng!");
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// Validation trong Editor
    /// </summary>
    private void OnValidate()
    {
        // Tự động tìm panel nếu chưa gán
        if (upgradePanel == null)
        {
            upgradePanel = FindObjectOfType<HeroUpgradePanel_Lv3>();
        }
    }
#endif
}
