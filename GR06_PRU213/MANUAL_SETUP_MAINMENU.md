# 🎯 HƯỚNG DẪN SETUP MAINMENU THỦ CÔNG - CHI TIẾT TỪNG BƯỚC

## ⚠️ QUAN TRỌNG: Làm TỪNG BƯỚC một, đừng skip!

---

## 📋 BƯỚC 1: CHUẨN BỊ (2 phút)

### 1.1 Mở scene MainMenu
- File → Open Scene
- Chọn `Assets/Scene/MainMenu.unity`

### 1.2 Backup scene (Quan trọng!)
- Ctrl+D để duplicate scene
- Đặt tên: `MainMenu_Backup.unity`

---

## 🎨 BƯỚC 2: TẠO TUTORIAL BUTTON (3 phút)

### 2.1 Duplicate nút New Game
1. Trong **Hierarchy**, tìm **NewGameButton**
2. Right-click → **Duplicate** (hoặc Ctrl+D)
3. Đổi tên thành: **TutorialButton**

### 2.2 Di chuyển vị trí
1. Chọn **TutorialButton**
2. Trong **Inspector** → **Rect Transform**
3. Thay đổi **Pos Y** (anchoredPosition Y):
   - Nếu NewGameButton có Pos Y = 0
   - Đặt TutorialButton Pos Y = -115
   - (Xuống dưới NewGameButton 115 pixels)

### 2.3 Đổi text
1. Trong **Hierarchy**, expand **TutorialButton**
2. Click vào **Text** (con của TutorialButton)
3. Trong **Inspector** → **TextMeshProUGUI** component
4. Đổi **Text** từ "New Game" thành: **"HOW TO PLAY"**

### 2.4 (Optional) Đổi màu nút
1. Chọn **TutorialButton**
2. Trong Inspector → **Button** component
3. Trong **Colors** → **Normal Color**
4. Đổi thành màu xanh dương: R:70, G:130, B:180

✅ **Test**: Bạn sẽ thấy 2 nút: "New Game" và "HOW TO PLAY"

---

## 📦 BƯỚC 3: TẠO TUTORIALMANAGER (2 phút)

### 3.1 Tạo Empty GameObject
1. Right-click trong **Hierarchy** → **Create Empty**
2. Đổi tên thành: **TutorialManager**

### 3.2 Add TutorialManager script
1. Chọn **TutorialManager** GameObject
2. Trong **Inspector**, click **Add Component**
3. Gõ: **TutorialManager**
4. Click để add script

### 3.3 Di chuyển vào Canvas
1. Kéo **TutorialManager** vào trong **Canvas** (làm con của Canvas)

✅ **Test**: TutorialManager xuất hiện trong Canvas với script attached

---

## 🎪 BƯỚC 4: TẠO LEVEL SELECTION PANEL (10 phút)

### 4.1 Tạo Panel
1. Right-click **Canvas** → **UI → Panel**
2. Đổi tên thành: **LevelSelectionPanel**

### 4.2 Setup Panel (Fullscreen overlay)
1. Chọn **LevelSelectionPanel**
2. Trong Inspector → **Rect Transform**:
   - Click icon Anchor Presets (góc trái trên của Rect Transform)
   - Giữ **Shift + Alt** và click vào **bottom-right** (stretch both)
   - Panel sẽ fill fullscreen

3. Trong Inspector → **Image** component:
   - Color: Black (R:0, G:0, B:0, A:220)
   - (Màu đen semi-transparent)

### 4.3 Tạo Title
1. Right-click **LevelSelectionPanel** → **UI → Text - TextMeshPro**
2. Đổi tên thành: **Title**
3. Setup:
   - **Text**: "SELECT LEVEL"
   - **Font Size**: 60
   - **Alignment**: Center (cả horizontal và vertical)
   - **Color**: White

4. Rect Transform:
   - Anchor: Top-Center
   - Pos X: 0
   - Pos Y: -100
   - Width: 600
   - Height: 80

### 4.4 Tạo Button Container
1. Right-click **LevelSelectionPanel** → **Create Empty**
2. Đổi tên: **LevelButtonsGroup**
3. Rect Transform:
   - Anchor: Center
   - Pos X: 0, Pos Y: 0
   - Width: 700, Height: 500

4. Add Component → **Grid Layout Group**:
   - Cell Size: X=200, Y=200
   - Spacing: X=30, Y=30
   - Constraint: Fixed Column Count = 3

### 4.5 Tạo 5 Level Buttons
**Làm 5 lần cho MAP 1, 2, 3, 4, 5:**

1. Right-click **LevelButtonsGroup** → **UI → Button - TextMeshPro**
2. Đổi tên: **Level1Button** (lần 2 là Level2Button, ...)
3. Setup Button:
   - Image Color: Orange (R:255, G:165, B:0)
4. Chọn **Text** (con của button):
   - Text: **"MAP 1"** (lần 2 là "MAP 2", ...)
   - Font Size: 36
   - Alignment: Center

**Lặp lại 5 lần để có:** Level1Button, Level2Button, Level3Button, Level4Button, Level5Button

### 4.6 Tạo Close Button
1. Right-click **LevelSelectionPanel** → **UI → Button - TextMeshPro**
2. Đổi tên: **CloseLevelSelectionButton**
3. Setup:
   - Text: **"CLOSE"**
   - Font Size: 32
   - Button Color: Red (R:200, G:50, B:50)

4. Rect Transform:
   - Anchor: Bottom-Center
   - Pos X: 0, Pos Y: 100
   - Width: 250, Height: 70

### 4.7 ẨN PANEL (QUAN TRỌNG!)
1. Chọn **LevelSelectionPanel** trong Hierarchy
2. Ở đầu Inspector, bên trái tên "LevelSelectionPanel" có **checkbox**
3. **UNCHECK** checkbox đó để ẨN panel

✅ **Test**: Panel biến mất khỏi Game view. Đúng rồi!

---

## 📚 BƯỚC 5: TẠO TUTORIAL PANEL (10 phút)

### 5.1 Tạo Panel
1. Right-click **Canvas** → **UI → Panel**
2. Đổi tên: **TutorialPanel**

### 5.2 Setup Panel (Fullscreen)
1. Rect Transform: Stretch fullscreen (Shift+Alt + click bottom-right anchor)
2. Image Color: Black (R:0, G:0, B:0, A:230)

### 5.3 Tạo Title
1. Right-click **TutorialPanel** → **UI → Text - TextMeshPro**
2. Đổi tên: **TitleText**
3. Setup:
   - Text: "Tutorial Title"
   - Font Size: 60
   - Alignment: Center
   - Color: Light Yellow (R:255, G:250, B:205)

4. Rect Transform:
   - Anchor: Top-Center
   - Pos X: 0, Pos Y: -80
   - Width: 800, Height: 100

### 5.4 Tạo Content Text
1. Right-click **TutorialPanel** → **UI → Text - TextMeshPro**
2. Đổi tên: **ContentText**
3. Setup:
   - Text: "Tutorial content will appear here..."
   - Font Size: 28
   - Alignment: Top-Left
   - Color: Light Gray (R:230, G:230, B:230)
   - **Wrapping**: Enabled

4. Rect Transform:
   - Anchor: Center
   - Pos X: 0, Pos Y: 0
   - Width: 900, Height: 500

### 5.5 Tạo Page Indicator
1. Right-click **TutorialPanel** → **UI → Text - TextMeshPro**
2. Đổi tên: **PageIndicatorText**
3. Setup:
   - Text: "Page 1 / 6"
   - Font Size: 28
   - Alignment: Center

4. Rect Transform:
   - Anchor: Bottom-Center
   - Pos X: 0, Pos Y: 100
   - Width: 200, Height: 60

### 5.6 Tạo Next Button
1. Right-click **TutorialPanel** → **UI → Button - TextMeshPro**
2. Đổi tên: **NextButton**
3. Setup:
   - Text: **"NEXT >"**
   - Font Size: 24
   - Button Color: Blue (R:70, G:130, B:180)

4. Rect Transform:
   - Anchor: Bottom-Center
   - Pos X: 250, Pos Y: 100
   - Width: 180, Height: 60

### 5.7 Tạo Previous Button
1. Right-click **TutorialPanel** → **UI → Button - TextMeshPro**
2. Đổi tên: **PrevButton**
3. Setup:
   - Text: **"< PREVIOUS"**
   - Font Size: 24
   - Button Color: Blue (R:70, G:130, B:180)

4. Rect Transform:
   - Anchor: Bottom-Center
   - Pos X: -250, Pos Y: 100
   - Width: 180, Height: 60

### 5.8 Tạo Close Button (X)
1. Right-click **TutorialPanel** → **UI → Button - TextMeshPro**
2. Đổi tên: **CloseTutorialButton**
3. Setup:
   - Text: **"X"**
   - Font Size: 40
   - Button Color: Red (R:200, G:50, B:50)

4. Rect Transform:
   - Anchor: Top-Right
   - Pos X: -50, Pos Y: -50
   - Width: 70, Height: 70

### 5.9 ẨN PANEL (QUAN TRỌNG!)
1. Chọn **TutorialPanel** trong Hierarchy
2. **UNCHECK checkbox** ở đầu Inspector

✅ **Test**: Panel biến mất khỏi Game view

---

## 🔌 BƯỚC 6: KẾT NỐI MAINMENUCONTROLLER (5 phút)

### 6.1 Tìm MainMenuController GameObject
- Trong Hierarchy, tìm GameObject có **MainMenuController** script
- (Có thể tên là "MainMenuController" hoặc nằm ở đâu đó trong Canvas)

### 6.2 Assign Main Buttons
Chọn MainMenuController GameObject, trong Inspector:

1. **Play Button**: Kéo **NewGameButton** (hoặc PlayButton) vào đây
2. **Tutorial Button**: Kéo **TutorialButton** vào đây
3. **Quit Button**: Kéo **QuitGameButton** vào đây

### 6.3 Assign Tutorial Manager
- **Tutorial Manager**: Kéo **TutorialManager** GameObject vào đây

### 6.4 Assign Level Selection Panel
- **Level Selection Panel**: Kéo **LevelSelectionPanel** vào đây
- **Close Level Selection Button**: Expand LevelSelectionPanel, kéo **CloseLevelSelectionButton** vào
- **Level Buttons** (Array size = 5):
  - Element 0: Kéo **Level1Button**
  - Element 1: Kéo **Level2Button**
  - Element 2: Kéo **Level3Button**
  - Element 3: Kéo **Level4Button**
  - Element 4: Kéo **Level5Button**

### 6.5 Assign Level Data Assets
- **All Levels** (Array size = 5):
  - Element 0: Kéo **Level1.asset** (Assets/Asset_map_1/ScriptableObjects/Level/)
  - Element 1: Kéo **Level2.asset**
  - Element 2: Kéa **Level3.asset**
  - Element 3: Kéo **Level4.asset**
  - Element 4: Kéo **Level5.asset**

---

## 🔌 BƯỚC 7: KẾT NỐI TUTORIALMANAGER (3 phút)

Chọn **TutorialManager** GameObject, trong Inspector:

1. **Tutorial Panel**: Kéo **TutorialPanel** vào
2. **Title Text**: Kéo **TitleText** vào
3. **Content Text**: Kéo **ContentText** vào
4. **Page Indicator Text**: Kéo **PageIndicatorText** vào
5. **Next Button**: Kéo **NextButton** vào
6. **Prev Button**: Kéo **PrevButton** vào
7. **Close Button**: Kéo **CloseTutorialButton** vào

---

## 💾 BƯỚC 8: LƯU VÀ TEST (1 phút)

### 8.1 Lưu Scene
- **Ctrl + S** (hoặc File → Save)

### 8.2 Test
1. Click **Play** ▶️
2. Thử các nút:
   - Click "New Game" (hoặc "PLAY") → Nếu có Level Selection Panel xuất hiện = OK
   - Click "HOW TO PLAY" → Tutorial Panel xuất hiện = OK
   - Click "X" hoặc "CLOSE" → Panels đóng = OK

---

## ✅ CHECKLIST HOÀN THÀNH

Sau khi làm xong, check list này:

```
□ TutorialButton đã tạo và hiển thị
□ TutorialManager GameObject đã tạo
□ LevelSelectionPanel đã tạo và ẨN (Active = false)
□ 5 Level Buttons (MAP 1-5) trong LevelSelectionPanel
□ CloseLevelSelectionButton đã tạo
□ TutorialPanel đã tạo và ẨN (Active = false)
□ TitleText, ContentText, PageIndicatorText trong TutorialPanel
□ NextButton, PrevButton, CloseTutorialButton trong TutorialPanel
□ MainMenuController đã assign đầy đủ references
□ TutorialManager đã assign đầy đủ references
□ 5 LevelData assets đã assign vào MainMenuController
□ Scene đã save
□ Test Play mode thành công
```

---

## ⚠️ LƯU Ý QUAN TRỌNG:

1. **Panels phải ẨN (Active = false)** khi không dùng
2. **Khi Test**, panels sẽ hiện ra khi click nút
3. Nếu panel không ẩn → Uncheck checkbox bên cạnh tên trong Inspector
4. **Save scene** sau mỗi bước lớn (Ctrl+S)

---

## 🐛 NẾU CÓ VẤN ĐỀ:

### Panels không ẩn:
→ Chọn panel → Uncheck checkbox ở đầu Inspector

### Click nút không hoạt động:
→ Check Console log xem lỗi gì
→ Check MainMenuController references đã assign đủ chưa

### Nút không tìm thấy:
→ Check Hierarchy xem GameObject có tên đúng không
→ Check GameObject nằm trong Canvas

---

**THỜI GIAN ƯỚC TÍNH: 30-40 phút nếu làm cẩn thận**

Good luck! Làm TỪNG BƯỚC một nhé! 🚀
