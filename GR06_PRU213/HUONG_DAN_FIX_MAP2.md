# 🇻🇳 HƯỚNG DẪN FIX MAP 2 - NÚT TUA TỐC ĐỘ

## ❌ VẤN ĐỀ:

Khi chơi Map 2, xuất hiện lỗi:
```
NullReferenceException: Object reference not set to an instance of an object
GameSpeedController.Start () (at Assets/Asset_map_2/Script/GameSpeedController.cs:21)
```

**Nguyên nhân:** Các nút tốc độ (0.5x, 1x, 2x) chưa được gán vào GameSpeedController

---

## ✅ GIẢI PHÁP TỰ ĐỘNG (KHUYÊN DÙNG):

### Bước 1: Mở Unity
- Mở project Tower Defense trong Unity
- Đợi Unity load xong hoàn toàn

### Bước 2: Kiểm tra script đã compile
- Nhìn thanh progress ở góc dưới phải Unity
- Đợi cho đến khi **"Compiling"** biến mất
- Thường mất 5-10 giây

### Bước 3: Chạy công cụ tự động
1. Nhìn lên menu bar Unity (File, Edit, Assets...)
2. Click vào **"Tools"**
3. Trong menu dropdown, tìm và click:
   ```
   Fix Map 2 Speed Controller 🔧
   ```
4. Một hộp thoại hiện ra với nội dung:
   ```
   Fix Map 2 Speed Controller
   
   Công cụ này sẽ tự động:
   1. Mở scene Game_Map2
   2. Tìm GameSpeedController
   3. Tìm và assign các nút tốc độ (0.5x, 1x, 2x)
   4. Save scene
   
   Bắt đầu fix?
   ```
5. Click nút **"YES, FIX IT!"**

### Bước 4: Đợi kết quả
- Tool sẽ tự động làm việc (3-5 giây)
- Một trong 2 kết quả sẽ xuất hiện:

#### Kết quả A: Thành công ✅
```
Success! 🎉

Đã fix GameSpeedController!

Buttons assigned:
• 0.5x: ✅
• 1x: ✅
• 2x: ✅

Scene đã được save.
Bây giờ bạn có thể test Map 2!
```
→ Click **"Awesome!"** → **XONG RỒI!** Chuyển sang BƯỚC 5

#### Kết quả B: Không tìm thấy buttons ⚠️
```
Warning

Không tìm thấy button nào!

Có thể:
1. Buttons chưa được tạo trong scene
2. Tên buttons không khớp với pattern tìm kiếm

Vui lòng kiểm tra lại UI trong scene Map 2.
```
→ Click **"OK"** → Chuyển sang **GIẢI PHÁP THỦ CÔNG** bên dưới

### Bước 5: Test Map 2
1. Trong Unity Hierarchy, tìm scene **Game_Map2** (hoặc mở từ Assets/Scene/Game_Map2.unity)
2. Click nút **Play** ▶️ ở giữa trên cùng
3. Khi game chạy, tìm các nút tốc độ trong UI
4. Thử click:
   - **"0.5x"** → Game chậm lại (enemies di chuyển chậm)
   - **"1x"** → Tốc độ bình thường
   - **"2x"** → Game nhanh gấp đôi (enemies chạy nhanh)
5. Kiểm tra Console (Ctrl+Shift+C hoặc Window → General → Console):
   - Phải thấy: `"Tốc độ game: 0.5x"` hoặc `"Tốc độ game: 2x"`
   - KHÔNG còn lỗi `NullReferenceException`
6. Click **Stop** ⏹️ để thoát Play mode

**✅ XONG! Map 2 đã được fix!**

---

## 🛠️ GIẢI PHÁP THỦ CÔNG (NẾU TỰ ĐỘNG KHÔNG WORK):

### Trường hợp 1: Buttons đã có trong scene

1. **Mở scene Game_Map2:**
   - File → Open Scene
   - Chọn Assets/Scene/Game_Map2.unity
   - Click Open

2. **Tìm GameSpeedController:**
   - Nhìn vào panel **Hierarchy** (bên trái)
   - Tìm GameObject có tên như: "GameManager", "UIManager", "SpeedController"
   - Click vào từng GameObject
   - Nhìn panel **Inspector** (bên phải)
   - Tìm component **"GameSpeedController"**

3. **Assign buttons:**
   - Khi đã tìm thấy GameSpeedController trong Inspector
   - Bạn sẽ thấy 3 ô trống:
     ```
     Speed 05x Button: None (Button)
     Speed 1x Button: None (Button)
     Speed 2x Button: None (Button)
     ```
   - Trong Hierarchy, tìm các button tốc độ (thường trong Canvas → UI)
   - Tìm button có text "0.5x" hoặc "x0.5"
   - **Kéo button đó** vào ô **"Speed 05x Button"**
   - Làm tương tự cho button "1x" và "2x"

4. **Save scene:**
   - Ctrl+S hoặc File → Save

5. **Test** (xem BƯỚC 5 ở trên)

---

### Trường hợp 2: Buttons chưa có trong scene (cần tạo mới)

#### Cách A: Copy từ Map 4 hoặc Map 5 (Dễ nhất)

1. **Mở scene Game_Map4:**
   - File → Open Scene
   - Chọn Assets/Scene/Game_Map4.unity

2. **Tìm UI Speed Control:**
   - Trong Hierarchy, mở **Canvas**
   - Tìm các button có text "0.5x", "1x", "2x"
   - Hoặc tìm parent object chứa các button này (VD: "SpeedControlPanel")

3. **Copy:**
   - Click chọn parent object (hoặc chọn cả 3 buttons - giữ Ctrl và click)
   - Ctrl+C (Copy)

4. **Switch sang Map 2:**
   - File → Open Scene
   - Chọn Game_Map2.unity

5. **Paste:**
   - Click chọn **Canvas** trong Hierarchy
   - Ctrl+V (Paste)
   - Buttons sẽ xuất hiện trong Canvas

6. **Adjust position:**
   - Click vào buttons vừa paste
   - Kéo đến vị trí phù hợp trên màn hình
   - (Có thể chỉnh trong Inspector → Rect Transform → Position)

7. **Assign vào GameSpeedController:**
   - Làm theo **Trường hợp 1 - Bước 2, 3, 4** ở trên

---

#### Cách B: Tạo mới từ đầu

1. **Tạo button thứ nhất:**
   - Trong Hierarchy, right-click vào **Canvas**
   - UI → Button - TextMeshPro
   - (Nếu lần đầu dùng TextMeshPro, click "Import TMP Essentials")
   - Đặt tên button: **"Button_Speed05x"**

2. **Đổi text:**
   - Mở button vừa tạo trong Hierarchy (click mũi tên bên cạnh)
   - Click vào **Text (TMP)** child object
   - Trong Inspector, tìm **"Text Input"**
   - Đổi text thành: **"0.5x"**

3. **Tạo 2 buttons còn lại:**
   - Copy button vừa tạo: Click chọn → Ctrl+D (Duplicate)
   - Đặt tên: **"Button_Speed1x"**
   - Đổi text thành: **"1x"**
   - Làm tương tự cho button thứ 3: **"Button_Speed2x"** với text **"2x"**

4. **Arrange buttons:**
   - Chọn cả 3 buttons (Giữ Ctrl và click)
   - Kéo đến góc màn hình bạn muốn (VD: góc trên phải)
   - Sắp xếp nằm ngang hoặc dọc

5. **Assign vào GameSpeedController:**
   - Làm theo **Trường hợp 1 - Bước 2, 3, 4** ở trên

---

## 📋 CHECKLIST HOÀN THÀNH:

Đánh dấu ✅ khi làm xong:

```
□ Unity đã mở và compile xong
□ Đã chạy Tools → Fix Map 2 Speed Controller 🔧
  HOẶC
□ Đã assign buttons thủ công vào GameSpeedController
□ Scene Game_Map2 đã save (Ctrl+S)
□ Đã test Play mode
□ Buttons hoạt động (click được, game thay đổi tốc độ)
□ Console không còn lỗi NullReferenceException
```

---

## 🐛 TROUBLESHOOTING - XỬ LÝ LỖI:

### Lỗi: "Tools menu không có Fix Map 2"
**Nguyên nhân:** Script chưa compile xong
**Fix:**
- Nhìn góc dưới phải Unity
- Đợi "Compiling" biến mất
- Thử lại sau 10 giây

### Lỗi: "Không tìm thấy GameSpeedController"
**Nguyên nhân:** Component chưa được add vào GameObject
**Fix:**
1. Tạo GameObject mới: Right-click Hierarchy → Create Empty
2. Đặt tên: "SpeedController"
3. Add component: Click GameObject → Inspector → Add Component → Tìm "GameSpeedController"
4. Assign buttons vào

### Lỗi: Buttons không đổi màu khi click
**Nguyên nhân:** Colors chưa được set
**Fix:**
1. Click GameObject có GameSpeedController
2. Trong Inspector, tìm:
   ```
   Normal Color: (Trắng)
   Selected Color: (Xanh hoặc màu khác)
   ```
3. Set 2 màu khác nhau

### Lỗi: Click button không có gì xảy ra
**Fix:**
1. Mở Console (Ctrl+Shift+C)
2. Click button trong Play mode
3. Xem có lỗi gì không?
4. Kiểm tra buttons đã assign đúng chưa
5. Kiểm tra Console có log "Tốc độ game: X.Xx" không

### Lỗi: Game không thay đổi tốc độ
**Nguyên nhân:** Time.timeScale không được áp dụng đúng
**Fix:**
1. Trong Play mode, mở Window → Analysis → Profiler
2. Click button tốc độ
3. Check xem Time.timeScale có thay đổi không
4. Nếu không → Check script GameSpeedController có đúng logic không

---

## 💡 NOTES QUAN TRỌNG:

1. **Map 1-3:** Có thể không có nút tua tốc độ → Đây là bình thường, chỉ Map 2 cần fix
2. **Map 4-5:** Có sẵn nút tua tốc độ hoạt động tốt → Có thể copy từ đây
3. **Time.timeScale:**
   - `Time.timeScale = 0.5` → Game chậm 50%
   - `Time.timeScale = 1.0` → Tốc độ bình thường (default)
   - `Time.timeScale = 2.0` → Game nhanh gấp đôi
   - `Time.timeScale = 0` → Game PAUSE (đóng băng hoàn toàn)
4. **Button Colors:** Nút được chọn sẽ đổi màu để user biết đang ở tốc độ nào
5. **Save Scene:** Luôn nhớ Ctrl+S sau khi thay đổi!

---

## 🎯 TEST NHANH:

Cách test nhanh nhất để biết đã fix thành công:

```
1. Mở scene Game_Map2
2. Click Play ▶️
3. Tìm button "2x" trên UI
4. Click vào
5. Nhìn enemies:
   - Nếu chạy NHANH GẤP ĐÔI → ✅ THÀNH CÔNG!
   - Nếu vẫn bình thường → ❌ Chưa fix được, xem lại
```

---

## 📞 CẦN TRỢ GIÚP?

Nếu làm theo hướng dẫn mà vẫn lỗi:

1. **Chụp màn hình:**
   - Console (Ctrl+Shift+C) - Để xem lỗi
   - Inspector của GameObject có GameSpeedController - Để xem references
   - Hierarchy - Để xem cấu trúc UI

2. **Ghi chú:**
   - Bạn làm đến bước nào?
   - Kết quả là gì? (Success hay Warning?)
   - Có thấy buttons trong scene không?

3. **Report lại** với thông tin trên

---

## ⏱️ THỜI GIAN:

- **Giải pháp tự động:** 30 giây - 1 phút
- **Giải pháp thủ công (TH1):** 2-3 phút
- **Giải pháp thủ công (TH2 - Cách A):** 5 phút
- **Giải pháp thủ công (TH2 - Cách B):** 10-15 phút

**→ Khuyên dùng giải pháp tự động trước!**

---

## 🚀 BẮT ĐẦU NGAY:

```
Tools → Fix Map 2 Speed Controller 🔧
```

**Chúc bạn thành công! 🎉**
