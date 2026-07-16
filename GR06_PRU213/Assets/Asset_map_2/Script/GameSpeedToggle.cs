using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Controller tốc độ game với 1 nút duy nhất
/// Click để chuyển đổi: 0.5x → 1x → 2x → 0.5x → ...
/// </summary>
public class GameSpeedToggle : MonoBehaviour
{
    [Header("Button Reference")]
    [SerializeField] private Button speedToggleButton;
    
    [Header("Text Display")]
    [SerializeField] private TextMeshProUGUI speedText;
    
    [Header("Speed Settings")]
    [SerializeField] private float[] speedLevels = { 0.5f, 1f, 2f };
    
    [Header("Visual Feedback")]
    [SerializeField] private Color normalSpeedColor = Color.white;
    [SerializeField] private Color slowSpeedColor = Color.yellow;
    [SerializeField] private Color fastSpeedColor = Color.green;
    
    private int currentSpeedIndex = 1; // Mặc định là 1x (index 1)

    void Start()
    {
        // Check null
        if (speedToggleButton == null)
        {
            Debug.LogError("[GameSpeedToggle] Speed Toggle Button chưa được assign!");
            return;
        }
        
        // Gán sự kiện click
        speedToggleButton.onClick.AddListener(CycleSpeed);
        
        // Set tốc độ ban đầu
        SetSpeed(currentSpeedIndex);
    }

    /// <summary>
    /// Chuyển sang tốc độ tiếp theo
    /// </summary>
    public void CycleSpeed()
    {
        // Tăng index, quay về 0 nếu vượt quá
        currentSpeedIndex = (currentSpeedIndex + 1) % speedLevels.Length;
        
        // Áp dụng tốc độ mới
        SetSpeed(currentSpeedIndex);
    }

    /// <summary>
    /// Set tốc độ game theo index
    /// </summary>
    private void SetSpeed(int index)
    {
        // Validate index
        if (index < 0 || index >= speedLevels.Length)
        {
            Debug.LogError($"[GameSpeedToggle] Invalid speed index: {index}");
            return;
        }
        
        float speed = speedLevels[index];
        
        // Thay đổi tốc độ game
        Time.timeScale = speed;
        
        // Update text hiển thị
        UpdateSpeedDisplay(speed);
        
        // Update màu nút
        UpdateButtonColor(speed);
        
        Debug.Log($"[GameSpeedToggle] Tốc độ game: {speed}x");
    }

    /// <summary>
    /// Cập nhật text hiển thị tốc độ
    /// </summary>
    private void UpdateSpeedDisplay(float speed)
    {
        if (speedText != null)
        {
            speedText.text = $"{speed:0.0}x";
        }
        else if (speedToggleButton != null)
        {
            // Fallback: Dùng text của button nếu không có speedText riêng
            var buttonText = speedToggleButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = $"{speed:0.0}x";
            }
        }
    }

    /// <summary>
    /// Cập nhật màu nút theo tốc độ
    /// </summary>
    private void UpdateButtonColor(float speed)
    {
        if (speedToggleButton == null) return;
        
        Color targetColor = normalSpeedColor;
        
        if (speed < 1f)
        {
            targetColor = slowSpeedColor; // Màu vàng cho chậm
        }
        else if (speed > 1f)
        {
            targetColor = fastSpeedColor; // Màu xanh cho nhanh
        }
        else
        {
            targetColor = normalSpeedColor; // Màu trắng cho bình thường
        }
        
        speedToggleButton.image.color = targetColor;
    }

    /// <summary>
    /// Reset về tốc độ bình thường khi disable
    /// </summary>
    void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
