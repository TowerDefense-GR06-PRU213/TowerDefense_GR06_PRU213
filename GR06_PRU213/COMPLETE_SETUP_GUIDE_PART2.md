# 🎯 HƯỚNG DẪN HOÀN THIỆN MAINMENU - PHẦN 2

## ✅ Bạn đã làm xong:
- ✅ Tạo TutorialButton
- ✅ Tạo TutorialManager GameObject
- ✅ Add TutorialManager script

---

## 📦 TIẾP TỤC: BƯỚC 4 - TẠO LEVEL SELECTION PANEL

### 4.1 Tạo Panel Background

1. **Right-click Canvas** trong Hierarchy
2. Chọn **UI → Panel**
3. Đổi tên thành: **`LevelSelectionPanel`**

### 4.2 Setup Panel Fullscreen

**Chọn LevelSelectionPanel**, trong Inspector:

#### Rect Transform:
1. Click vào **Anchor Presets** (icon hình vuông nhỏ góc trên bên trái của Rect Transform)
2. Giữ **Shift + Alt** (cả 2 phím cùng lúc)
3. Click vào ô **bottom-right** (góc dưới bên phải) của grid
4. Panel sẽ stretch ra fullscreen

#### Image Component:
1. Tìm **Image** component (dưới Rect Transform)
2. Click vào **Color**
3. Đặt màu:
   - **R: 0**
   - **G: 0**
   - **B: 0**
   - **A: 220** (transparency)
   - Hoặc dùng color picker chọn màu đen, rồi kéo Alpha xuống ~220

### 4.3 Tạo Title "SELECT LEVEL"

1. **Right-click LevelSelectionPanel** trong Hierarchy
2. Chọn **UI → Text - TextMeshPro**
   - Nếu lần đầu dùng TMP, sẽ có popup "Import TMP Essentials" → Click **Import TMP Essentials**
3. Đổi tên Text thành: **`Title`**

**Setup Title:**

#### Rect Transform:
- **Anchor**: Top-Center
  - Click Anchor Presets
  - Click vào ô **top-center** (giữa hàng trên)
- **Pos X**: `0`
- **Pos Y**: `-100`
- **Width**: `600`
- **Height**: `80`

#### TextMeshProUGUI Component:
- **Text**: `SELECT LEVEL`
- **Font Size**: `60`
- **Alignment**: 
  - Horizontal: Center (icon ở giữa)
  - Vertical: Middle (icon ở giữa)
- **Color**: White (R:255, G:255, B:255)

### 4.4 Tạo Container cho Level Buttons

1. **Right-click LevelSelectionPanel**
2. **Create Empty**
3. Đổi tên: **`LevelButtonsGroup`**

**Setup Container:**

#### Rect Transform:
- **Anchor**: Center
  - Click Anchor Presets → chọn ô **center** (ô giữa)
- **Pos X**: `0`
- **Pos Y**: `0`
- **Width**: `700`
- **Height**: `500`

#### Add Grid Layout Group:
1. Click **Add Component**
2. Gõ: `Grid Layout Group`
3. Click để add

**Settings của Grid Layout Group:**
- **Cell Size**: X = `200`, Y = `200`
- **Spacing**: X = `30`, Y = `30`
- **Start Corner**: Upper Left
- **Start Axis**: Horizontal
- **Child Alignment**: Middle Center
- **Constraint**: `Fixed Column Count`
- **Constraint Count**: `3`

### 4.5 Tạo 5 Level Buttons (MAP 1-5)

**LÀM 5 LẦN - cho từng map:**

#### Tạo Button 1 (MAP 1):

1. **Right-click LevelButtonsGroup**
2. **UI → Button - TextMeshPro**
3. Đổi tên: **`Level1Button`**

**Setup Button:**
- Trong Inspector → **Image** component:
  - **Color**: Orange
    - R: `255`
    - G: `165`
    - B: `0`
    - A: `255`

**Setup Text (con của button):**
1. **Expand Level1Button** trong Hierarchy (click mũi tên)
2. Click vào **Text (TMP)** (con của Level1Button)
3. Trong Inspector:
   - **Text**: `MAP 1`
   - **Font Size**: `36`
   - **Alignment**: Center (cả horizontal và vertical)
   - **Color**: White

#### Lặp lại 4 lần nữa:

Tạo thêm 4 buttons tương tự:
- **Level2Button** → Text: `MAP 2`
- **Level3Button** → Text: `MAP 3`
- **Level4Button** → Text: `MAP 4`
- **Level5Button** → Text: `MAP 5`

**Mẹo nhanh**: Duplicate Level1Button (Ctrl+D) 4 lần, rồi chỉ cần đổi tên và text!

### 4.6 Tạo Close Button

1. **Right-click LevelSelectionPanel**
2. **UI → Button - TextMeshPro**
3. Đổi tên: **`CloseLevelSelectionButton`**

**Setup Close Button:**

#### Rect Transform:
- **Anchor**: Bottom-Center
- **Pos X**: `0`
- **Pos Y**: `100`
- **Width**: `250`
- **Height**: `70`

#### Image Component:
- **Color**: Red
  - R: `200`
  - G: `50`
  - B: `50`

#### Text:
- **Text**: `CLOSE`
- **Font Size**: `32`
- **Alignment**: Center
- **Color**: White

### 4.7 ẨN PANEL (QUAN TRỌNG!)

**Chọn LevelSelectionPanel** trong Hierarchy:

1. Nhìn lên đầu Inspector
2. Bên trái tên "LevelSelectionPanel" có **1 checkbox nhỏ** (đang checked)
3. **CLICK VÀO CHECKBOX ĐÓ** để uncheck
4. Panel sẽ biến mất trong Game view → ĐÚNG RỒI!

**✅ XONG BƯỚC 4!** Level Selection Panel đã hoàn thành!

---

## 📚 BƯỚC 5 - TẠO TUTORIAL PANEL

### 5.1 Tạo Panel

1. **Right-click Canvas**
2. **UI → Panel**
3. Đổi tên: **`TutorialPanel`**

### 5.2 Setup Panel Fullscreen

**Chọn TutorialPanel:**

#### Rect Transform:
- Giữ **Shift + Alt**, click **bottom-right** anchor preset
- Panel stretch fullscreen

#### Image:
- **Color**: Black (R:0, G:0, B:0, A:230)

### 5.3 Tạo Title Text

1. **Right-click TutorialPanel**
2. **UI → Text - TextMeshPro**
3. Đổi tên: **`TitleText`**

**Setup:**

#### Rect Transform:
- **Anchor**: Top-Center
- **Pos X**: `0`
- **Pos Y**: `-80`
- **Width**: `800`
- **Height**: `100`

#### TextMeshProUGUI:
- **Text**: `Tutorial Title`
- **Font Size**: `60`
- **Alignment**: Center
- **Color**: Light Yellow (R:255, G:250, B:205)

### 5.4 Tạo Content Text

1. **Right-click TutorialPanel**
2. **UI → Text - TextMeshPro**
3. Đổi tên: **`ContentText`**

**Setup:**

#### Rect Transform:
- **Anchor**: Center
- **Pos X**: `0`
- **Pos Y**: `0`
- **Width**: `900`
- **Height**: `500`

#### TextMeshProUGUI:
- **Text**: `Tutorial content will appear here...`
- **Font Size**: `28`
- **Alignment**: Top-Left
- **Color**: Light Gray (R:230, G:230, B:230)
- **Wrapping**: Enabled (check box "Wrap Text")

### 5.5 Tạo Page Indicator

1. **Right-click TutorialPanel**
2. **UI → Text - TextMeshPro**
3. Đổi tên: **`PageIndicatorText`**

**Setup:**

#### Rect Transform:
- **Anchor**: Bottom-Center
- **Pos X**: `0`
- **Pos Y**: `100`
- **Width**: `200`
- **Height**: `60`

#### TextMeshProUGUI:
- **Text**: `Page 1 / 6`
- **Font Size**: `28`
- **Alignment**: Center
- **Color**: White

### 5.6 Tạo Next Button

1. **Right-click TutorialPanel**
2. **UI → Button - TextMeshPro**
3. Đổi tên: **`NextButton`**

**Setup:**

#### Rect Transform:
- **Anchor**: Bottom-Center
- **Pos X**: `250` (bên phải page indicator)
- **Pos Y**: `100`
- **Width**: `180`
- **Height**: `60`

#### Image:
- **Color**: Blue (R:70, G:130, B:180)

#### Text:
- **Text**: `NEXT >`
- **Font Size**: `24`
- **Alignment**: Center

### 5.7 Tạo Previous Button

1. **Right-click TutorialPanel**
2. **UI → Button - TextMeshPro**
3. Đổi tên: **`PrevButton`**

**Setup:**

#### Rect Transform:
- **Anchor**: Bottom-Center
- **Pos X**: `-250` (bên trái page indicator)
- **Pos Y**: `100`
- **Width**: `180`
- **Height**: `60`

#### Image:
- **Color**: Blue (R:70, G:130, B:180)

#### Text:
- **Text**: `< PREVIOUS`
- **Font Size**: `24`
- **Alignment**: Center

### 5.8 Tạo Close Button (X)

1. **Right-click TutorialPanel**
2. **UI → Button - TextMeshPro**
3. Đổi tên: **`CloseTutorialButton`**

**Setup:**

#### Rect Transform:
- **Anchor**: Top-Right
- **Pos X**: `-50`
- **Pos Y**: `-50`
- **Width**: `70`
- **Height**: `70`

#### Image:
- **Color**: Red (R:200, G:50, B:50)

#### Text:
- **Text**: `X`
- **Font Size**: `40`
- **Alignment**: Center

### 5.9 ẨN PANEL

**Chọn TutorialPanel** → **Uncheck checkbox** ở đầu Inspector

**✅ XONG BƯỚC 5!** Tutorial Panel hoàn thành!

---

## 🔌 BƯỚC 6 - KẾT NỐI MAINMENUCONTROLLER

### 6.1 Tìm MainMenuController

Trong Hierarchy, tìm GameObject có **MainMenuController** script.
- Có thể ở trong Canvas
- Hoặc là GameObject riêng

**Nếu KHÔNG TÌM THẤY:**
1. Chọn **Canvas**
2. Trong Inspector, click **Add Component**
3. Gõ: `MainMenuController`
4. Click để add

### 6.2 Assign References

**Chọn GameObject có MainMenuController**, trong Inspector tìm MainMenuController component:

#### Main Buttons:
- **Play Button**: 
  - Kéo **NewGameButton** (hoặc PlayButton) từ Hierarchy vào đây
- **Tutorial Button**: 
  - Kéo **TutorialButton** từ Hierarchy vào đây
- **Quit Button**: 
  - Kéo **QuitGameButton** từ Hierarchy vào đây

#### Tutorial Manager:
- **Tutorial Manager**: 
  - Kéo **TutorialManager** GameObject vào đây

#### Level Selection Panel:
- **Level Selection Panel**: 
  - Kéo **LevelSelectionPanel** vào đây
  
- **Close Level Selection Button**: 
  - Expand **LevelSelectionPanel** trong Hierarchy
  - Kéo **CloseLevelSelectionButton** vào đây

- **Level Buttons** (mở rộng array):
  - Set **Size** = `5`
  - **Element 0**: Kéo **Level1Button**
  - **Element 1**: Kéo **Level2Button**
  - **Element 2**: Kéo **Level3Button**
  - **Element 3**: Kéo **Level4Button**
  - **Element 4**: Kéo **Level5Button**

#### Level Data:
- **All Levels** (mở rộng array):
  - Set **Size** = `5`
  
**Trong Project panel, navigate:**
`Assets/Asset_map_1/ScriptableObjects/Level/`

Kéo các files:
  - **Element 0**: Kéo **Level1.asset**
  - **Element 1**: Kéo **Level2.asset**
  - **Element 2**: Kéo **Level3.asset**
  - **Element 3**: Kéo **Level4.asset**
  - **Element 4**: Kéo **Level5.asset**

**✅ XONG BƯỚC 6!**

---

## 🔌 BƯỚC 7 - KẾT NỐI TUTORIALMANAGER

**Chọn TutorialManager GameObject**, trong Inspector:

### Assign UI References:

- **Tutorial Panel**: 
  - Kéo **TutorialPanel** vào
  
- **Title Text**: 
  - Expand TutorialPanel trong Hierarchy
  - Kéo **TitleText** vào
  
- **Content Text**: 
  - Kéo **ContentText** vào
  
- **Illustration Image**: 
  - Để trống (hoặc tạo GameObject Image nếu muốn có ảnh)
  
- **Next Button**: 
  - Kéo **NextButton** vào
  
- **Prev Button**: 
  - Kéo **PrevButton** vào
  
- **Close Button**: 
  - Kéo **CloseTutorialButton** vào
  
- **Page Indicator Text**: 
  - Kéo **PageIndicatorText** vào

**✅ XONG BƯỚC 7!**

---

## 💾 BƯỚC 8 - LƯU VÀ TEST

### 8.1 Lưu Scene
- **Ctrl + S** (hoặc File → Save)

### 8.2 Test trong Play Mode

1. Click **Play** ▶️
2. Test các chức năng:

#### Test New Game / Play:
- Click nút "New Game" hoặc "PLAY"
- Nếu có Level Selection Panel hiển thị → ✅ OK
- Nếu load trực tiếp Map 1 → ✅ Cũng OK

#### Test Tutorial:
- Click "HOW TO PLAY"
- Tutorial Panel hiện ra → ✅ OK
- Click "NEXT >" → Chuyển trang → ✅ OK
- Click "< PREVIOUS" → Quay lại → ✅ OK
- Click "X" → Panel đóng → ✅ OK

#### Test Level Selection:
- Click "PLAY"
- Click "MAP 1" → Load Game_Map1 → ✅ OK
- Thử các map khác

### 8.3 Debug nếu có vấn đề

**Nếu click button không hoạt động:**
1. Mở **Console** (Window → General → Console)
2. Xem lỗi gì
3. Thường là do thiếu reference

**Check Console log:**
- Nếu có lỗi "TutorialManager not assigned" → Check MainMenuController references
- Nếu có lỗi "Panel not assigned" → Check TutorialManager references

---

## ✅ CHECKLIST CUỐI CÙNG

```
□ LevelSelectionPanel đã tạo và ẨN
□ 5 Level Buttons (MAP 1-5)
□ CloseLevelSelectionButton
□ TutorialPanel đã tạo và ẨN
□ TitleText, ContentText, PageIndicatorText
□ NextButton, PrevButton, CloseTutorialButton
□ MainMenuController đã assign đầy đủ:
  □ Play Button
  □ Tutorial Button
  □ Quit Button
  □ Tutorial Manager
  □ Level Selection Panel
  □ Close Level Selection Button
  □ 5 Level Buttons
  □ 5 LevelData assets
□ TutorialManager đã assign đầy đủ:
  □ Tutorial Panel
  □ Title Text
  □ Content Text
  □ Next/Prev/Close Buttons
  □ Page Indicator Text
□ Scene đã save
□ Test thành công
```

---

## 🎉 HOÀN THÀNH!

Nếu tất cả test đều pass → **BẠN ĐÃ HOÀN THÀNH MAINMENU REDESIGN!** 🎉

---

## 📞 NẾU GẶP VẤN ĐỀ:

1. **Panels không ẩn**: 
   - Chọn panel → Uncheck checkbox ở đầu Inspector

2. **Click button không hoạt động**: 
   - Check Console log
   - Check references đã assign đủ chưa

3. **Script không tìm thấy**: 
   - Đợi Unity compile xong
   - Check file có tồn tại không

4. **UI bị lỗi layout**: 
   - Check Rect Transform settings
   - Check Anchor đúng chưa

Nếu vẫn không được, chụp màn hình lỗi và báo tôi! 🚀
