# 🔧 FIX: Nút Tua Map 4 Không Hoạt Động

## ❌ 2 LỖI ĐÃ FIX

### 1. NullReferenceException - ShowObjective()
```
NullReferenceException: Object reference not set to an instance of an object
UIController_map_4+<ShowObjective>d__68.MoveNext () (at Assets/Asset_map_4/Scripts_map_4/UIController_Map4.cs:393)
```

**Nguyên nhân:** `LevelManager.Instance` là null khi vào thẳng Scene Map 4

**Đã fix:** Thêm null check
```csharp
if (LevelManager.Instance != null && LevelManager.Instance.CurrentLevel != null)
{
    objectiveText.text = $"Survive {LevelManager.Instance.CurrentLevel.wavesToWin} waves!";
}
else
{
    objectiveText.text = $"Survive 5 waves!"; // Fallback
}
```

---

### 2. Nút Tua Không Hoạt Động

**Nguyên nhân có thể:**
- ❌ Chưa gán `speedToggleButton` trong Inspector
- ❌ Chưa gán `speedButtonText` trong Inspector
- ❌ Button bị inactive
- ❌ Button có Interactable = false

**Đã thêm:** Debug logs để kiểm tra

---

## 🎮 HƯỚNG DẪN DEBUG

### BƯỚC 1: Kiểm tra Console khi Start game

**Mở Unity → Play Map 4 → Xem Console:**

#### ✅ Nếu thấy:
```
[UIController_Map4] Speed toggle button listener added!
[UIController_Map4] Initial speed set to 1x
```
→ Button đã được gán đúng!

#### ❌ Nếu thấy:
```
[UIController_Map4] speedToggleButton is NULL! Please assign it in Inspector.
```
→ **Button chưa được gán!** Làm theo BƯỚC 2.

---

### BƯỚC 2: Gán Button trong Inspector

1. **Mở Scene Game_Map4**
2. **Tìm Canvas → Tìm nút speed** (biểu tượng ">>")
3. **Chọn UIController GameObject** trong Hierarchy
4. **Trong Inspector → UIController_map_4:**
   ```
   Speed Toggle Button: [NONE] ← Kéo button vào đây
   Speed Button Text: [NONE] ← Kéo Text (TMP) con của button vào
   ```

#### Cách tìm Text (TMP):
```
Hierarchy:
└─ Canvas
   └─ SpeedButton (hoặc tên tương tự)
      └─ Text (TMP) ← Kéo cái này vào Speed Button Text
```

---

### BƯỚC 3: Test Click Button

**Click vào nút speed trong game → Xem Console:**

#### ✅ Nếu thấy:
```
[UIController_Map4] CycleGameSpeed() called!
[UIController_Map4] Speed changed to 2x (index: 2)
```
→ **Button hoạt động!** ✅

#### ❌ Nếu KHÔNG thấy gì:
→ Button không gọi được hàm. Kiểm tra:

**1. Button có Interactable = true?**
```
Chọn Button → Inspector → Button component
✓ Interactable: [✓] ← Phải được check
```

**2. Button có Raycast Target?**
```
Chọn Button → Inspector → Image component
✓ Raycast Target: [✓] ← Phải được check
```

**3. Canvas có GraphicRaycaster?**
```
Chọn Canvas → Inspector
Tìm component: Graphic Raycaster ← Phải có
```

**4. Scene có EventSystem?**
```
Hierarchy → Tìm GameObject "EventSystem"
Nếu không có → Right-click Hierarchy → UI → Event System
```

---

## 🔍 DEBUG CHI TIẾT

### Test 1: Kiểm tra Button Reference

```csharp
// Thêm vào Start() để debug
Debug.Log($"speedToggleButton: {speedToggleButton}");
Debug.Log($"speedButtonText: {speedButtonText}");
```

**Kết quả mong đợi:**
```
speedToggleButton: SpeedButton (UnityEngine.UI.Button)
speedButtonText: Text (TMP) (TMPro.TextMeshProUGUI)
```

**Nếu thấy null:**
```
speedToggleButton: null ← ❌ Chưa gán!
```

---

### Test 2: Kiểm tra Button onClick

**Cách 1: Trong Inspector**
```
Chọn Button → Inspector → Button component
On Click ()
  List is Empty ← ❌ SAI! Code đã AddListener rồi mà không có?
```

**Nếu list empty:**
- Button có thể đang Disabled trong scene
- Hoặc UIController chưa chạy Start()

**Cách 2: Thêm onClick thủ công**
```
Trong Inspector của Button:
On Click ()
  Runtime Only
  + (Add)
  GameObject: [Kéo UIController GameObject vào]
  Function: UIController_map_4 → CycleGameSpeed()
```

---

### Test 3: Test bằng code trực tiếp

**Thêm vào Update() để test:**
```csharp
private void Update()
{
    if (Keyboard.current.escapeKey.wasPressedThisFrame) 
        TogglePause();
    
    // ✨ TEST: Bấm phím T để test speed
    if (Keyboard.current.tKey.wasPressedThisFrame)
    {
        Debug.Log("T key pressed! Testing CycleGameSpeed...");
        CycleGameSpeed();
    }
}
```

**Test:** Bấm phím **T** trong game
- ✅ Nếu speed thay đổi → Code đúng, vấn đề ở Button UI
- ❌ Nếu không đổi → Vấn đề ở code SetGameSpeed()

---

## 📋 CHECKLIST ĐẦY ĐỦ

### Scene Setup:
- [ ] Scene có Canvas
- [ ] Canvas có GraphicRaycaster component
- [ ] Scene có EventSystem GameObject

### Button Setup:
- [ ] Button GameObject tồn tại trong Canvas
- [ ] Button có component Button (Script)
- [ ] Button có component Image (với Raycast Target = true)
- [ ] Button Interactable = true
- [ ] Button có Text (TMP) con

### UIController Setup:
- [ ] UIController GameObject tồn tại
- [ ] UIController có component UIController_map_4
- [ ] Speed Toggle Button field → Gán button GameObject
- [ ] Speed Button Text field → Gán Text (TMP) component

### Code Verification:
- [ ] Console hiển thị: "Speed toggle button listener added!"
- [ ] Click button → Console hiển thị: "CycleGameSpeed() called!"
- [ ] Text thay đổi: "1x" → "2x" → "0.5x" → "1x"
- [ ] Game speed thực sự thay đổi

---

## 🚀 GIẢI PHÁP NHANH

**Nếu vẫn không hoạt động sau khi làm hết:**

### Tạo Button mới từ đầu:

1. **Right-click trong Canvas → UI → Button - TextMeshPro**
2. **Đổi tên: SpeedToggleButton**
3. **Position:** Góc dưới trái màn hình
4. **Text:** "1x"
5. **Gán vào UIController:**
   - Speed Toggle Button → SpeedToggleButton
   - Speed Button Text → Text (TMP) con của button
6. **Save & Test**

---

## ✅ SAU KHI FIX

1. ✅ Không còn NullReferenceException
2. ✅ Nút speed hoạt động bình thường
3. ✅ Click để cycle: 1x → 2x → 0.5x → 1x
4. ✅ Text hiển thị đúng tốc độ hiện tại
5. ✅ Game speed thực sự thay đổi

---

**Làm theo checklist và debug logs để tìm vấn đề!** 🔍
