# 🚀 TỰ ĐỘNG SETUP MAINMENU - CỰC KỲ ĐƠN GIẢN!

## ✅ ĐÃ TẠO XONG SCRIPT TỰ ĐỘNG!

---

## 📋 CÁCH SỬ DỤNG (3 BƯỚC):

### BƯỚC 1: Quay lại Unity
- Alt+Tab để quay về Unity
- Đợi Unity compile (vài giây)

### BƯỚC 2: Chạy script tự động
1. Click menu **Tools** trong Unity menu bar
2. Chọn **"Auto Setup MainMenu NOW! 🚀"**
3. Một dialog hiện ra → Click **"YES!"**
4. Đợi vài giây...
5. Sẽ có dialog "Success! 🎉" → Click **"Awesome!"**

### BƯỚC 3: Lưu và Test
1. **Save scene**: Ctrl+S
2. **Play**: Nhấn ▶️
3. Test các nút:
   - Click "HOW TO PLAY" → Tutorial Panel hiện
   - Click "PLAY" hoặc "New Game" → Nếu có Level Selection hiện lên

---

## ✨ SCRIPT SẼ TỰ ĐỘNG TẠO:

✅ **Tutorial Button** - Nút "HOW TO PLAY"
✅ **TutorialManager** - GameObject quản lý tutorial
✅ **Level Selection Panel** - Panel chọn 5 maps (ẨN sẵn)
✅ **Tutorial Panel** - Panel hướng dẫn (ẨN sẵn)
✅ **Tất cả UI elements** - Buttons, texts, navigation

---

## ⚠️ SAU KHI CHẠY SCRIPT:

### Bạn CẦN ASSIGN REFERENCES THỦ CÔNG:

**Chọn GameObject có MainMenuController** trong Inspector:

#### 1. Main Buttons:
- **Play Button**: Kéo **PlayButton** (hoặc NewGameButton)
- **Tutorial Button**: Kéo **TutorialButton**
- **Quit Button**: Kéo **QuitGameButton**

#### 2. Tutorial Manager:
- **Tutorial Manager**: Kéo **TutorialManager** GameObject

#### 3. Level Selection Panel:
- **Level Selection Panel**: Kéo **LevelSelectionPanel**
- **Close Level Selection Button**: Kéo **CloseLevelSelectionButton**
- **Level Buttons** (Array size = 5):
  - Element 0-4: Kéo Level1Button, Level2Button, Level3Button, Level4Button, Level5Button

#### 4. Level Data:
- **All Levels** (Array size = 5):
  - Element 0-4: Kéo Level1.asset → Level5.asset
  - (Từ Assets/Asset_map_1/ScriptableObjects/Level/)

---

### Assign TutorialManager References:

**Chọn TutorialManager GameObject** trong Inspector:

- **Tutorial Panel**: Kéo **TutorialPanel**
- **Title Text**: Kéo **TitleText** (trong TutorialPanel)
- **Content Text**: Kéo **ContentText**
- **Next Button**: Kéo **NextButton**
- **Prev Button**: Kéo **PrevButton**
- **Close Button**: Kéo **CloseTutorialButton**
- **Page Indicator Text**: Kéo **PageIndicatorText**

---

## ✅ CHECKLIST:

```
□ Script đã chạy thành công (Tools → Auto Setup MainMenu NOW!)
□ TutorialButton đã tạo
□ TutorialManager đã tạo
□ LevelSelectionPanel đã tạo và ẨN
□ TutorialPanel đã tạo và ẨN
□ MainMenuController references đã assign
□ TutorialManager references đã assign
□ Scene đã save
□ Test Play mode thành công
```

---

## 🎯 TEST:

### 1. Click Play ▶️
### 2. Thử các nút:
- "HOW TO PLAY" → Tutorial Panel hiện → ✅
- "NEXT >" → Chuyển trang → ✅
- "X" → Panel đóng → ✅
- "PLAY" → Load game hoặc Level Selection → ✅

---

## 🐛 NẾU CÓ LỖI:

### "Script not found" / Menu không có:
→ Đợi Unity compile xong (check thanh progress dưới)

### Panels không ẩn:
→ Chọn panel → Uncheck checkbox ở đầu Inspector

### Click button không hoạt động:
→ Check Console log
→ Check references đã assign chưa

### Panel hiện fullscreen che hết:
→ Panels đúng là fullscreen overlay
→ Nhưng phải ẨN (Active = false) ban đầu
→ Script đã auto ẩn rồi, nếu vẫn hiện thì uncheck manually

---

## 💡 MẸO:

1. **Luôn save scene** sau mỗi thay đổi lớn (Ctrl+S)
2. **Test ngay** sau khi assign references
3. **Check Console** nếu có vấn đề
4. Nếu script chạy lỗi, có thể **chạy lại** - nó sẽ skip những gì đã tạo

---

## 🎉 KẾT QUẢ MONG ĐỢI:

Sau khi làm xong:
- ✅ MainMenu có nút "HOW TO PLAY"
- ✅ Click vào hiện Tutorial Panel
- ✅ Tutorial có navigation (Next/Prev)
- ✅ Level Selection có 5 map buttons
- ✅ Tất cả panels ẨN khi start
- ✅ Click nút mới panels hiện ra

---

**THỜI GIAN:**
- Chạy script: 5 giây
- Assign references: 3-5 phút
- **TỔNG: ~5 phút!** 🚀

---

**BẮT ĐẦU: Tools → Auto Setup MainMenu NOW! 🚀**

---

## 🆕 MỚI: FIX MAP 2 SPEED CONTROLLER

Nếu Map 2 bị lỗi nút tua tốc độ:

**Tools → Fix Map 2 Speed Controller 🔧**

Chi tiết: Xem file `FIX_MAP2_SPEED_CONTROLLER.md`

---

Good luck! 🎮
