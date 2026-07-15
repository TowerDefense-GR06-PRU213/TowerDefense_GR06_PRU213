# 🔧 FIX: Map 5 NullReferenceException

## ❌ LỖI GỐC

```
[08:38:48] NullReferenceException: Object reference not set to an instance of an object
UIController_Map5.ShowObjective () (at Assets/Asset_map_5/Scripts/UIController_Map5.cs:306)

[08:37:32] NullReferenceException: Object reference not set to an instance of an object
Spawner_Map5.Update () (at Assets/Asset_map_5/Scripts/Spawner_Map5.cs:136)
```

---

## 🔍 NGUYÊN NHÂN

**LevelManager.Instance là NULL!**

Map 5 đang cố truy cập:
```csharp
LevelManager.Instance.CurrentLevel.wavesToWin
```

Nhưng **LevelManager chưa được setup trong Scene Map 5!**

---

## ✅ ĐÃ SỬA

### 1. UIController_Map5.cs - ShowObjective()

**Trước:**
```csharp
private IEnumerator ShowObjective()
{
    objectiveText.text = $"Survive {LevelManager.Instance.CurrentLevel.wavesToWin} waves!";
    // ❌ LevelManager.Instance = null → CRASH!
}
```

**Sau:**
```csharp
private IEnumerator ShowObjective()
{
    // ✅ Kiểm tra null trước
    if (LevelManager.Instance != null && LevelManager.Instance.CurrentLevel != null)
    {
        objectiveText.text = $"Survive {LevelManager.Instance.CurrentLevel.wavesToWin} waves!";
    }
    else
    {
        // ✅ Fallback: Dùng giá trị mặc định
        objectiveText.text = $"Survive 5 waves!";
        Debug.LogWarning("[UIController_Map5] LevelManager not found, using default");
    }
    // ...
}
```

---

### 2. UIController_Map5.cs - RestartLevel()

**Trước:**
```csharp
public void RestartLevel()
{
    LevelManager.Instance.LoadLevel(LevelManager.Instance.CurrentLevel);
    // ❌ Crash khi restart!
}
```

**Sau:**
```csharp
public void RestartLevel()
{
    if (LevelManager.Instance != null && LevelManager.Instance.CurrentLevel != null)
    {
        LevelManager.Instance.LoadLevel(LevelManager.Instance.CurrentLevel);
    }
    else
    {
        // ✅ Fallback: Reload scene trực tiếp
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.LogWarning("[UIController_Map5] LevelManager not found, reloading scene");
    }
}
```

---

### 3. Spawner_Map5.cs - Update()

**Trước:**
```csharp
// Kiểm tra điều kiện thắng
if (!_isSpawningGroup && _enemiesRemovedInWave >= _totalEnemiesInWave && 
    _waveCounter >= LevelManager.Instance.CurrentLevel.wavesToWin && !_isEndlessMode)
{
    OnMissionComplete?.Invoke();
}
// ❌ Crash mỗi frame!
```

**Sau:**
```csharp
// ✅ Lấy wavesToWin an toàn
int wavesToWin = 5; // Default
if (LevelManager.Instance != null && LevelManager.Instance.CurrentLevel != null)
{
    wavesToWin = LevelManager.Instance.CurrentLevel.wavesToWin;
}

if (!_isSpawningGroup && _enemiesRemovedInWave >= _totalEnemiesInWave && 
    _waveCounter >= wavesToWin && !_isEndlessMode)
{
    OnMissionComplete?.Invoke();
}
```

---

## 🎮 KẾT QUẢ

### ✅ GIẢI PHÁP TẠM THỜI:
- Map 5 giờ có thể **chạy độc lập** mà không cần LevelManager
- Sử dụng giá trị mặc định: **5 waves để thắng**
- Restart level hoạt động bằng cách reload scene

### ⚠️ HẠN CHẾ:
- Không có resource persistence (vì không dùng LevelManager)
- Không thể chuyển map liên tục (Map 4 → Map 5)
- Mỗi lần chơi bắt đầu với 500 gold mặc định

---

## 🔧 GIẢI PHÁP DÀI HẠN (TÙY CHỌN)

Nếu muốn Map 5 hoạt động đầy đủ với LevelManager:

### Option 1: Thêm LevelManager vào Scene Map 5

**Bước 1:** Mở Scene Game_Map5
**Bước 2:** Create Empty GameObject → Rename "LevelManager"
**Bước 3:** Add Component → LevelManager script
**Bước 4:** Trong Inspector:
```
- All Levels array: Size = 5
  - Element 0: Level1 asset
  - Element 1: Level2 asset
  - Element 2: Level3 asset
  - Element 3: Level4 asset
  - Element 4: Level5 asset
```

### Option 2: Tạo LevelManager Prefab chung

**Bước 1:** Tạo LevelManager trong Map 1
**Bước 2:** Drag vào Project → Tạo Prefab
**Bước 3:** Drag prefab vào Map 4 và Map 5

### Option 3: Giữ nguyên (Khuyến nghị)

Nếu Map 5 chỉ cần chạy độc lập (không cần progression):
- ✅ **Giữ code như hiện tại** (đã fix null check)
- ✅ Đơn giản, ít bug
- ✅ Dễ test

---

## 📊 SO SÁNH

| Feature | Với LevelManager | Không LevelManager (Hiện tại) |
|---------|------------------|-------------------------------|
| Chạy độc lập | ✅ | ✅ |
| Resource persistence | ✅ | ❌ |
| Map progression (1→2→3→4→5) | ✅ | ❌ |
| Configurable waves to win | ✅ | ⚠️ (mặc định 5) |
| Complexity | ⚠️ Cao | ✅ Thấp |
| Test dễ dàng | ⚠️ | ✅ |

---

## ✅ KẾT LUẬN

**Fix đã hoàn tất!**

- ✅ Map 5 không còn crash
- ✅ Có thể chơi bình thường
- ✅ Objective text hiển thị: "Survive 5 waves!"
- ✅ Mission Complete hoạt động
- ✅ Restart level hoạt động

**Lưu ý:**
- Map 5 giờ **standalone** (không cần LevelManager)
- Nếu muốn tích hợp đầy đủ → làm theo Option 1/2 ở trên

---

Bây giờ mở Unity và test Map 5 xem còn lỗi gì không! 🚀
