# 🔄 HƯỚNG DẪN: Đổi 3 Nút Speed Thành 1 Nút Cycle

## 📋 YÊU CẦU

Thay đổi từ:
```
[0.5x] [1x] [2x]  ← 3 nút riêng biệt
```

Thành:
```
[>>] hoặc [1x]  ← 1 nút duy nhất, click để cycle: 0.5x → 1x → 2x → 0.5x...
```

---

## 🔧 MAP 4 - UIController_map_4.cs

### BƯỚC 1: Thay đổi biến (dòng ~48)

**XÓA:**
```csharp
[Header("Speed Buttons")]
[SerializeField] private Button speed1Button;
[SerializeField] private Button speed2Button;
[SerializeField] private Button speed3Button;
[SerializeField] private Button pauseButton;
[SerializeField] private Button nextLevelButton;

[SerializeField] private Color normalButtonColor = Color.white;
[SerializeField] private Color selectedButtonColor = Color.blue;
[SerializeField] private Color normalTextColor = Color.black;
[SerializeField] private Color selectedTextColor = Color.white;
```

**THAY BẰNG:**
```csharp
[Header("Speed Button")]
[SerializeField] private Button speedToggleButton; // NÚT DUY NHẤT
[SerializeField] private TMP_Text speedButtonText; // Text hiển thị: "0.5x", "1x", "2x"
[SerializeField] private Button pauseButton;
[SerializeField] private Button nextLevelButton;

// ✨ Quản lý tốc độ
private float[] speedLevels = { 0.5f, 1f, 2f }; // 3 mức tốc độ
private int currentSpeedIndex = 1; // Bắt đầu từ 1x (index 1)
```

---

### BƯỚC 2: Thay đổi Start() (dòng ~131)

**XÓA:**
```csharp
speed1Button.onClick.AddListener(() => SetGameSpeed(0.2f));
speed2Button.onClick.AddListener(() => SetGameSpeed(1f));
speed3Button.onClick.AddListener(() => SetGameSpeed(2f));
HighlightSelectedSpeedButton(GameManager_map_4.Instance.GameSpeed);
```

**THAY BẰNG:**
```csharp
// ✨ GÁN SỰ KIỆN CHO NÚT SPEED TOGGLE
if (speedToggleButton != null)
{
    speedToggleButton.onClick.AddListener(CycleGameSpeed);
}

// Khởi tạo tốc độ ban đầu
SetGameSpeed(speedLevels[currentSpeedIndex]); // 1x
UpdateSpeedButtonText();
```

---

### BƯỚC 3: Thay đổi Speed Functions (dòng ~267)

**XÓA:**
```csharp
private void SetGameSpeed(float timeScale)
{
    HighlightSelectedSpeedButton(timeScale);
    GameManager_map_4.Instance.SetGameSpeed(timeScale);
}

private void UpdateButtonVisual(Button button, bool isSelected)
{
    button.image.color = isSelected ? selectedButtonColor : normalButtonColor;
    var text = button.GetComponentInChildren<TMP_Text>();
    if (text) text.color = isSelected ? selectedTextColor : normalTextColor;
}

private void HighlightSelectedSpeedButton(float selectedSpeed)
{
    UpdateButtonVisual(speed1Button, selectedSpeed == 0.2f);
    UpdateButtonVisual(speed2Button, selectedSpeed == 1f);
    UpdateButtonVisual(speed3Button, selectedSpeed == 2f);
}
```

**THAY BẰNG:**
```csharp
// ✨ HÀM MỚI: Cycle qua 3 tốc độ
private void CycleGameSpeed()
{
    // Tăng index, quay lại 0 nếu vượt quá
    currentSpeedIndex = (currentSpeedIndex + 1) % speedLevels.Length;
    
    // Áp dụng tốc độ mới
    float newSpeed = speedLevels[currentSpeedIndex];
    SetGameSpeed(newSpeed);
    
    // Cập nhật text trên button
    UpdateSpeedButtonText();
    
    Debug.Log($"[UIController_Map4] Speed changed to {newSpeed}x");
}

// ✨ HÀM MỚI: Cập nhật text hiển thị trên button
private void UpdateSpeedButtonText()
{
    if (speedButtonText != null)
    {
        float currentSpeed = speedLevels[currentSpeedIndex];
        speedButtonText.text = $"{currentSpeed}x";
    }
}

private void SetGameSpeed(float timeScale)
{
    GameManager_map_4.Instance.SetGameSpeed(timeScale);
}
```

---

## 🔧 MAP 5 - UIController_Map5.cs

### BƯỚC 1: Thay đổi biến (dòng ~22)

**XÓA:**
```csharp
[SerializeField] private Button speed1Button;
[SerializeField] private Button speed2Button;
[SerializeField] private Button speed3Button;
[SerializeField] private Button muteButton;

[SerializeField] private Color normalButtonColor = Color.white;
[SerializeField] private Color selectedButtonColor = Color.blue;
[SerializeField] private Color normalTextColor = Color.black;
[SerializeField] private Color selectedTextColor = Color.white;
```

**THAY BẰNG:**
```csharp
[SerializeField] private Button speedToggleButton; // NÚT DUY NHẤT  
[SerializeField] private TMP_Text speedButtonText; // Text hiển thị tốc độ
[SerializeField] private Button muteButton;

// ✨ Quản lý tốc độ
private float[] speedLevels = { 0.5f, 1f, 2f };
private int currentSpeedIndex = 1; // Bắt đầu từ 1x
```

---

### BƯỚC 2: Thay đổi Start() (dòng ~62)

**XÓA:**
```csharp
speed1Button.onClick.AddListener(() => SetGameSpeed(0.2f));
speed2Button.onClick.AddListener(() => SetGameSpeed(1f));
speed3Button.onClick.AddListener(() => SetGameSpeed(2f));

HighlightSelectedSpeedButton(GameManager_Map5.Instance.GameSpeed);
```

**THAY BẰNG:**
```csharp
// ✨ GÁN SỰ KIỆN CHO NÚT SPEED TOGGLE
if (speedToggleButton != null)
{
    speedToggleButton.onClick.AddListener(CycleGameSpeed);
}

// Khởi tạo tốc độ ban đầu
SetGameSpeed(speedLevels[currentSpeedIndex]); // 1x
UpdateSpeedButtonText();
```

---

### BƯỚC 3: Thay đổi Speed Functions (dòng ~194)

**XÓA:**
```csharp
private void SetGameSpeed(float timeScale)
{
    HighlightSelectedSpeedButton(timeScale);
    GameManager_Map5.Instance.SetGameSpeed(timeScale);
}

private void UpdateButtonVisual(Button button, bool isSelected)
{
    button.image.color = isSelected ? selectedButtonColor : normalButtonColor;

    TMP_Text text = button.GetComponentInChildren<TMP_Text>();
    if (text != null)
    {
        text.color = isSelected ? selectedTextColor : normalTextColor;
    }
}

private void HighlightSelectedSpeedButton(float selectedSpeed)
{
    UpdateButtonVisual(speed1Button, selectedSpeed == 0.2f);
    UpdateButtonVisual(speed2Button, selectedSpeed == 1f);
    UpdateButtonVisual(speed3Button, selectedSpeed == 2f);
}
```

**THAY BẰNG:**
```csharp
// ✨ HÀM MỚI: Cycle qua 3 tốc độ
private void CycleGameSpeed()
{
    currentSpeedIndex = (currentSpeedIndex + 1) % speedLevels.Length;
    float newSpeed = speedLevels[currentSpeedIndex];
    SetGameSpeed(newSpeed);
    UpdateSpeedButtonText();
    Debug.Log($"[UIController_Map5] Speed changed to {newSpeed}x");
}

// ✨ HÀM MỚI: Cập nhật text hiển thị
private void UpdateSpeedButtonText()
{
    if (speedButtonText != null)
    {
        float currentSpeed = speedLevels[currentSpeedIndex];
        speedButtonText.text = $"{currentSpeed}x";
    }
}

private void SetGameSpeed(float timeScale)
{
    GameManager_Map5.Instance.SetGameSpeed(timeScale);
}
```

---

## 🎮 SETUP TRONG UNITY

### Map 4:

1. **Mở Scene Game_Map4**
2. **Tìm Canvas → Tìm 3 nút speed cũ**
3. **Xóa 2 nút** (giữ lại 1 nút)
4. **Chọn nút còn lại:**
   - Đổi tên: `SpeedToggleButton`
   - Thêm **Text (TMP)** con vào button (nếu chưa có)
   - Text hiển thị: `1x`
5. **Chọn UIController GameObject:**
   - Inspector → UIController_map_4 component
   - **Speed Toggle Button**: Kéo `SpeedToggleButton` vào
   - **Speed Button Text**: Kéo Text (TMP) con của button vào
6. **Save Scene**

### Map 5:

Làm tương tự như Map 4.

---

## 🔍 DEBUG

### Test trong Unity:

1. **Press Play**
2. **Click nút Speed:**
   - Lần 1: `1x` → `2x`
   - Lần 2: `2x` → `0.5x`
   - Lần 3: `0.5x` → `1x`
3. **Kiểm tra Console:**
   ```
   [UIController_Map4] Speed changed to 2x
   [UIController_Map4] Speed changed to 0.5x
   [UIController_Map4] Speed changed to 1x
   ```

### Nếu có lỗi:

**Lỗi: speedToggleButton is null**
- Fix: Gán button vào Inspector

**Lỗi: speedButtonText is null**
- Fix: Gán Text (TMP) vào Inspector
- Hoặc: Bỏ qua (button sẽ không hiển thị text)

**Text không cập nhật:**
- Fix: Đảm bảo Text component là **TextMeshPro (TMP_Text)**, không phải Text cũ

---

## ✅ KẾT QUẢ

- ✅ 1 nút duy nhất thay vì 3 nút
- ✅ Click để cycle: 0.5x → 1x → 2x → 0.5x
- ✅ Text hiển thị tốc độ hiện tại
- ✅ Code gọn gàng hơn
- ✅ UX tốt hơn (ít clutter)

---

## 📝 LƯU Ý

1. **Icon thay đổi theo tốc độ** (tùy chọn):
   ```csharp
   [SerializeField] private Sprite[] speedIcons; // 3 icons cho 3 tốc độ
   
   private void UpdateSpeedButtonText()
   {
       if (speedButtonText != null)
       {
           speedButtonText.text = $"{speedLevels[currentSpeedIndex]}x";
       }
       
       // Thay đổi icon
       if (speedIcons.Length == 3 && speedToggleButton != null)
       {
           speedToggleButton.image.sprite = speedIcons[currentSpeedIndex];
       }
   }
   ```

2. **Animation khi click** (tùy chọn):
   ```csharp
   private void CycleGameSpeed()
   {
       // ... existing code ...
       
       // Thêm animation
       speedToggleButton.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f);
   }
   ```

3. **Save tốc độ khi thoát game** (tùy chọn):
   ```csharp
   private void OnApplicationQuit()
   {
       PlayerPrefs.SetInt("SpeedIndex", currentSpeedIndex);
   }
   
   private void Start()
   {
       currentSpeedIndex = PlayerPrefs.GetInt("SpeedIndex", 1);
       // ... rest of code ...
   }
   ```

---

Làm theo hướng dẫn này là xong! 🚀
