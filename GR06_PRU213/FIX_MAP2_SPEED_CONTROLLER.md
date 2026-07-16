# 🔧 FIX MAP 2 - NÚT TUA TỐC ĐỘ

## ⚡ CÁCH FIX TỰ ĐỘNG (1 PHÚT):

### BƯỚC 1: Quay lại Unity
- Alt+Tab về Unity
- Đợi Unity compile script mới (vài giây)

### BƯỚC 2: Chạy Auto-Fix
1. Click menu **Tools** trong Unity menu bar
2. Chọn **"Fix Map 2 Speed Controller 🔧"**
3. Dialog hiện ra → Click **"YES, FIX IT!"**
4. Đợi vài giây...
5. Xem kết quả:
   - ✅ Nếu thành công → "Success! 🎉" 
   - ⚠️ Nếu không tìm thấy buttons → Xem CÁCH FIX THỦ CÔNG bên dưới

### BƯỚC 3: Test Map 2
1. Play scene **Game_Map2** (▶️)
2. Thử các nút tốc độ:
   - Click "0.5x" → Game chậm lại
   - Click "1x" → Tốc độ bình thường
   - Click "2x" → Game nhanh gấp đôi
3. Xem Console log: "Tốc độ game: X.Xx"

---

## 🛠️ CÁCH FIX THỦ CÔNG (NẾU AUTO-FIX KHÔNG WORK):

### TH1: Buttons đã có sẵn trong scene

1. **Mở scene Game_Map2**
2. **Tìm GameObject có GameSpeedController component**
   - Tìm trong Hierarchy (thường là "GameManager" hoặc "UIController")
3. **Trong Inspector, assign 3 buttons:**
   - **Speed 05x Button**: Kéo button "0.5x" speed vào đây
   - **Speed 1x Button**: Kéo button "1x" speed vào đây
   - **Speed 2x Button**: Kéo button "2x" speed vào đây
4. **Save scene** (Ctrl+S)
5. **Test Play**

---

### TH2: Buttons chưa có, cần tạo mới

Nếu Map 2 chưa có UI buttons cho speed control:

#### Giải pháp A: Copy từ Map 4/5 (RECOMMENDED)

1. **Mở scene Game_Map4** hoặc **Game_Map5**
2. **Tìm Speed Control UI** (thường có nút tua tốc độ)
3. **Copy toàn bộ UI elements** liên quan
4. **Switch sang scene Game_Map2**
5. **Paste** (Ctrl+V)
6. **Adjust position** cho phù hợp
7. **Assign vào GameSpeedController**

#### Giải pháp B: Tạo mới từ đầu

1. **Tạo 3 buttons** trong Canvas:
   - Right-click Canvas → UI → Button - TextMeshPro
   - Đặt tên: "Button_Speed05x", "Button_Speed1x", "Button_Speed2x"
2. **Đổi text:**
   - Button 1: "0.5x"
   - Button 2: "1x"
   - Button 3: "2x"
3. **Arrange buttons** (đặt cạnh nhau)
4. **Assign vào GameSpeedController:**
   - Kéo 3 buttons vào 3 slots tương ứng
5. **Save và test**

---

## 📋 CHECKLIST:

```
□ Unity đã compile xong script FixMap2SpeedController.cs
□ Đã chạy Tools → Fix Map 2 Speed Controller 🔧
□ Hoặc đã assign buttons thủ công
□ Scene Game_Map2 đã save
□ Test Play mode: Buttons hoạt động
□ Console không còn lỗi NullReferenceException
```

---

## ✅ KẾT QUẢ MONG ĐỢI:

Sau khi fix xong:
- ✅ Map 2 có 3 nút tốc độ: 0.5x, 1x, 2x
- ✅ Click các nút → Game thay đổi tốc độ
- ✅ Nút được bấm đổi màu (highlight)
- ✅ Console log: "Tốc độ game: X.Xx"
- ✅ Không còn lỗi NullReferenceException

---

## 🐛 NẾU VẪN LỖI:

### Lỗi: "Không tìm thấy GameSpeedController"
**Fix:** GameSpeedController component chưa được add vào GameObject nào
- Tạo một GameObject mới (VD: "SpeedController")
- Add Component → GameSpeedController
- Assign buttons vào

### Lỗi: "Không tìm thấy buttons"
**Fix:** Buttons chưa được tạo trong scene
- Làm theo **Giải pháp A** hoặc **B** ở trên

### Buttons không đổi màu khi click
**Fix:** Colors chưa được set
- Chọn GameObject có GameSpeedController
- Set **Normal Color** = White
- Set **Selected Color** = Blue (hoặc màu khác)

### Click button không có gì xảy ra
**Fix:** 
1. Check Console log → Có lỗi gì không?
2. Check buttons đã được assign chưa?
3. Check Time.timeScale có thay đổi không? (Debug.Log)

---

## 💡 LƯU Ý:

- **Map 1-3** có thể không có nút tua tốc độ → Bình thường
- **Map 4-5** có sẵn nút tua tốc độ → Tham khảo cách làm
- GameSpeedController thay đổi **Time.timeScale** để điều chỉnh tốc độ game
- Time.timeScale = 0.5 → Chậm 50%
- Time.timeScale = 1.0 → Bình thường
- Time.timeScale = 2.0 → Nhanh gấp đôi

---

## 🎯 QUICK TEST:

```
1. Play Map 2 (▶️)
2. Click button "2x"
3. Xem enemies di chuyển nhanh hơn → ✅ SUCCESS!
```

---

**BẮT ĐẦU: Tools → Fix Map 2 Speed Controller 🔧**

Good luck! 🚀
