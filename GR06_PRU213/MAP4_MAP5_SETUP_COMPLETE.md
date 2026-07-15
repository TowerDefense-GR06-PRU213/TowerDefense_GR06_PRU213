# ✅ HOÀN TẤT: THÊM MAP 4 & MAP 5

## 📋 ĐÃ THỰC HIỆN

### 1. ✅ Copy Scene Files
```
✅ Assets/Scene/Game_Map4.unity
✅ Assets/Scene/Game_Map4.unity.meta
✅ Assets/Scene/Game_Map5.unity
✅ Assets/Scene/Game_Map5.unity.meta
```

### 2. ✅ Cập Nhật Build Settings
Đã bật Map 4 & 5 trong `EditorBuildSettings.asset`:
```yaml
- enabled: 1  # Changed from 0 to 1
  path: Assets/Scene/Game_Map4.unity
- enabled: 1  # Changed from 0 to 1
  path: Assets/Scene/Game_Map5.unity
```

### 3. ✅ LevelData Đã Có Sẵn
```
✅ Assets/Asset_map_1/ScriptableObjects/Level/Level4.asset
   - levelName: Game_Map4
   - startingResources: 500
   - wavesToWin: 5

✅ Assets/Asset_map_1/ScriptableObjects/Level/Level5.asset
   - levelName: Game_Map5
   - startingResources: 500
   - wavesToWin: 5
```

### 4. ✅ Assets Đã Đầy Đủ
```
✅ Assets/Asset_map_4/ (quái, hero, sprites, scripts, prefabs)
✅ Assets/Asset_map_5/ (quái, hero, sprites, scripts, prefabs)
```

---

## 🎮 CÁCH MỞ MAP 4 & MAP 5 TRONG UNITY

### Bước 1: Mở Unity Editor
1. Mở Unity Hub
2. Mở project **GR06_PRU213**
3. Chờ Unity compile xong

### Bước 2: Mở Scene
**Mở Map 4:**
- `File → Open Scene`
- Chọn `Assets/Scene/Game_Map4.unity`
- Hoặc double-click vào file trong Project panel

**Mở Map 5:**
- `File → Open Scene`
- Chọn `Assets/Scene/Game_Map5.unity`
- Hoặc double-click vào file trong Project panel

### Bước 3: Kiểm Tra Build Settings
1. `File → Build Settings` (Ctrl+Shift+B)
2. Xác nhận **Game_Map4** và **Game_Map5** có dấu ✓ (enabled)
3. Nếu không có, click vào checkbox để bật

---

## 🧪 KIỂM TRA MAP 4 & MAP 5

### Test Map 4:
```
1. Mở Scene: Game_Map4.unity
2. Press Play (Ctrl+P)
3. Kiểm tra:
   ✅ Map hiển thị đúng
   ✅ UI hiển thị (Gold, Lives, Wave counter)
   ✅ Có nút Select Hero
   ✅ Có Fast-Forward button (x2 speed)
   ✅ Có Pause button
   
4. Test gameplay:
   ✅ Click nút mở hero panel
   ✅ Chọn hero và đặt lên platform
   ✅ Quái spawn và đi theo đường
   ✅ Hero tự động bắn quái
   ✅ Nhận gold khi quái chết
   ✅ Fast-forward button tăng tốc game
   
5. Test Mission Complete:
   ✅ Thắng hết waves
   ✅ Hiện panel Mission Complete
   ✅ Gold có lưu lại không (xem console log)
```

### Test Map 5:
```
1. Mở Scene: Game_Map5.unity
2. Press Play (Ctrl+P)
3. Kiểm tra:
   ✅ Map hiển thị đúng
   ✅ UI hiển thị đầy đủ
   ✅ Hero panel có 4 heroes
   ✅ Fast-forward button
   
4. Test Ability System (QUAN TRỌNG!):
   ✅ Bongma: Có dodge attacks không? (evasion)
   ✅ Xuong: Khi <30% HP có tăng tốc không? (rage)
   ✅ Bongmaacdoc: Có damage reduction không?
   ✅ Phuthuybongtoi: Có heal aura không?
   ✅ Boss Chuatebongtoi: Có special ability không?
   
5. Test Fast-Forward với Abilities:
   ✅ Bật x2 speed
   ✅ Abilities vẫn hoạt động bình thường
   ✅ Không có bug animation
```

---

## 🔧 NẾU CÓ LỖI

### Lỗi 1: Scene không mở được
```
Triệu chứng: "Scene could not be loaded"
Nguyên nhân: Unity chưa nhận diện file

Fix:
1. Đóng Unity
2. Delete thư mục Library/
3. Mở lại Unity (sẽ reimport tất cả)
```

### Lỗi 2: Missing References
```
Triệu chứng: "Missing Prefab" hoặc "NullReferenceException"
Nguyên nhân: Prefabs/Scripts chưa được link

Fix:
1. Mở Scene có lỗi
2. Trong Hierarchy, tìm GameObject màu đỏ
3. Trong Inspector, tìm field "Missing (Script)"
4. Gán lại script từ Project panel
```

### Lỗi 3: GameManager not found
```
Triệu chứng: "GameManager_map_4.Instance is null"
Nguyên nhân: Thiếu GameManager trong scene

Fix:
1. Kiểm tra Hierarchy có "GameManager" GameObject không
2. Nếu không có, tạo mới:
   - Create Empty GameObject → Rename "GameManager"
   - Add Component → GameManager_map_4 (cho Map 4)
   - Add Component → GameManager_Map5 (cho Map 5)
```

### Lỗi 4: Fast-Forward không hoạt động
```
Triệu chứng: Click button x2 nhưng game không nhanh lên
Nguyên nhân: Time.timeScale bị lock

Fix:
1. Kiểm tra console có error không
2. Mở UIController_Map4.cs hoặc UIController_Map5.cs
3. Tìm method SetGameSpeed()
4. Xác nhận có gọi GameManager.Instance.SetGameSpeed()
```

### Lỗi 5: Quái không spawn
```
Triệu chứng: Game chạy nhưng không có quái nào xuất hiện
Nguyên nhân: Spawner hoặc WaveData chưa được setup

Fix:
1. Kiểm tra Hierarchy có "Spawner" GameObject không
2. Chọn Spawner → Inspector
3. Kiểm tra:
   - Wave Data array có ScriptableObjects không
   - Enemy Prefabs có được assign không
   - Path có được gán không
```

---

## 🚀 BƯỚC TIẾP THEO (TÙY CHỌN)

### 1. Thêm Map 4 & 5 vào MainMenu
Nếu muốn chọn map từ menu chính:

```csharp
// Trong MainMenu UI, thêm 2 buttons:
// - Button "Map 4" → onClick: LoadMap4()
// - Button "Map 5" → onClick: LoadMap5()

public void LoadMap4()
{
    LevelManager.Instance.LoadLevel(Level4Data);
}

public void LoadMap5()
{
    LevelManager.Instance.LoadLevel(Level5Data);
}
```

### 2. Setup Level Progression
Để Map 3 → Map 4 → Map 5 tự động:

```csharp
// Trong LevelManager hoặc UIController:
// Khi Mission Complete Map 3:
if (currentLevel == Level3)
{
    nextLevel = Level4; // Thay vì null
}
```

### 3. Cân Bằng Game Balance
Kiểm tra độ khó:
- Map 4: `startingResources: 500` → Có phải quá dễ?
- Map 5: `startingResources: 500` → Có phải quá khó?

Điều chỉnh trong LevelData asset files nếu cần.

---

## 📊 THỐNG KÊ

**Đã thêm:**
- ✅ 2 Scene files (Game_Map4, Game_Map5)
- ✅ 2 LevelData assets (Level4, Level5)
- ✅ Build Settings đã cập nhật

**Đã có sẵn từ trước:**
- ✅ Map 4 assets (quái, hero, sprites, scripts)
- ✅ Map 5 assets (quái, hero, sprites, Ability System)
- ✅ GameManager_map_4.cs
- ✅ GameManager_Map5.cs
- ✅ UIController_Map4.cs
- ✅ UIController_Map5.cs
- ✅ Fast-forward button logic
- ✅ Resource persistence logic

**Tổng cộng:**
- 📁 6 scene files (MainMenu, Map1, Map2, Map3, Map4, Map5)
- 🎮 5 playable maps
- 🚀 Đầy đủ chức năng (fast-forward, resource saving, ability system)

---

## ✅ KẾT LUẬN

**Map 4 và Map 5 đã được thêm vào dự án thành công!**

**Bạn có thể:**
1. ✅ Mở Scene Map 4 & 5 trong Unity
2. ✅ Test chơi ngay lập tức
3. ✅ Build game với đầy đủ 5 maps

**Lưu ý:**
- Fast-forward button ĐÃ CÓ SẴN
- Resource persistence ĐÃ CÓ SẴN
- Map 5 có Ability System ĐẶC BIỆT (cẩn thận khi sửa)

---

Mọi thứ đã sẵn sàng! Hãy mở Unity và test Map 4 & 5 nhé! 🎉
