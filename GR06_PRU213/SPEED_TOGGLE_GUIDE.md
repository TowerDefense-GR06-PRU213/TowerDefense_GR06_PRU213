# 🔄 NÚT TỐC ĐỘ TOGGLE - 1 NÚT DUY NHẤT

## ✨ TÍNH NĂNG MỚI:

Thay vì 3 nút riêng biệt (0.5x, 1x, 2x), giờ chỉ cần **1 NÚT DUY NHẤT**!

**Click lần 1:** 0.5x (Chậm) 🐌 → Nút màu **VÀNG**
**Click lần 2:** 1.0x (Bình thường) ⚡ → Nút màu **TRẮNG**
**Click lần 3:** 2.0x (Nhanh) 🚀 → Nút màu **XANH**
**Click lần 4:** Quay lại 0.5x → Lặp lại...

---

## 🚀 SETUP TỰ ĐỘNG (30 GIÂY):

### Bước 1: Quay lại Unity
- Alt+Tab về Unity
- Đợi compile xong (5-10 giây)

### Bước 2: Chạy Auto-Setup
1. Click menu **Tools**
2. Chọn **"Setup Map 2 Speed Toggle (1 Button) 🔄"**
3. Click **"YES, DO IT!"**
4. Đợi "Success! 🎉"

### Bước 3: Test
1. Play Map 2 ▶️
2. Tìm nút tốc độ (hiển thị "1.0x")
3. Click vào:
   - **Lần 1:** 2.0x → Màu xanh → Enemies chạy nhanh gấp đôi ✅
   - **Lần 2:** 0.5x → Màu vàng → Enemies chạy chậm 50% ✅
   - **Lần 3:** 1.0x → Màu trắng → Tốc độ bình thường ✅
4. Click tiếp → Lặp lại cycle

**✅ XONG!**

---

## 📋 TOOL SẼ TỰ ĐỘNG:

✅ Mở scene Game_Map2
✅ Tìm hoặc tạo 1 button duy nhất
✅ Add component **GameSpeedToggle**
✅ Setup tất cả references
✅ Xóa **GameSpeedController** cũ (3 buttons)
✅ Đặt button ở góc trên phải màn hình
✅ Save scene

---

## 🎨 VISUAL FEEDBACK:

Button sẽ tự động đổi màu theo tốc độ:

| Tốc độ | Màu sắc | Text | Ý nghĩa |
|--------|---------|------|---------|
| 0.5x | 🟡 Vàng | "0.5x" | Chậm - Dễ điều khiển |
| 1.0x | ⚪ Trắng | "1.0x" | Bình thường |
| 2.0x | 🟢 Xanh | "2.0x" | Nhanh - Tiết kiệm thời gian |

---

## 🔧 CÁCH HOẠT ĐỘNG:

### Script: GameSpeedToggle.cs

**Thuộc tính:**
- `speedToggleButton` → Reference đến button
- `speedText` → Text hiển thị "X.Xx"
- `speedLevels` → Array tốc độ [0.5, 1.0, 2.0]
- `normalSpeedColor` → Màu cho 1x (Trắng)
- `slowSpeedColor` → Màu cho 0.5x (Vàng)
- `fastSpeedColor` → Màu cho 2x (Xanh)

**Logic:**
1. Click button → `CycleSpeed()` được gọi
2. `currentSpeedIndex++` và wrap về 0 nếu vượt quá
3. `Time.timeScale = speedLevels[currentSpeedIndex]`
4. Update text và màu button
5. Log ra Console

---

## 🛠️ CUSTOM TỐC ĐỘ (TÙY CHỌN):

Nếu muốn thay đổi các mức tốc độ:

1. **Mở scene Game_Map2**
2. **Tìm button** có component **GameSpeedToggle**
3. **Trong Inspector**, tìm **Speed Levels**:
   ```
   Speed Levels (Array)
   Size: 3
   Element 0: 0.5
   Element 1: 1.0
   Element 2: 2.0
   ```
4. **Thay đổi giá trị:**
   - Muốn thêm mức 1.5x? → Size = 4, Element 3 = 1.5
   - Muốn tốc độ cực nhanh 5x? → Element 2 = 5.0
   - Muốn bỏ chậm? → Đổi Element 0 = 1.0
5. **Save scene** (Ctrl+S)

**Ví dụ tùy chỉnh:**
```
Size: 4
Element 0: 0.5  → Chậm
Element 1: 1.0  → Bình thường
Element 2: 1.5  → Hơi nhanh
Element 3: 3.0  → Cực nhanh
```

---

## 🎯 SO SÁNH 2 HỆ THỐNG:

### HỆ THỐNG CŨ (GameSpeedController):
❌ 3 buttons riêng biệt
❌ Tốn không gian UI
❌ User phải tìm button đúng
✅ Rõ ràng, dễ hiểu

### HỆ THỐNG MỚI (GameSpeedToggle):
✅ 1 button duy nhất
✅ Tiết kiệm không gian UI
✅ Click liên tục để cycle
✅ Màu sắc thay đổi trực quan
✅ Text hiển thị rõ ràng
⚠️ User cần học cách dùng (nhưng rất đơn giản)

**→ Hệ thống mới phù hợp với mobile và UI gọn gàng!**

---

## 📱 TƯƠNG TỰ MAP 4 & MAP 5:

Map 4 và Map 5 đã có hệ thống tương tự:
- **Map 4:** Toggle pause/play với visual feedback
- **Map 5:** Speed toggle với các mức tốc độ

Giờ **Map 2** cũng có chức năng tương tự! 🎉

---

## 🐛 TROUBLESHOOTING:

### Tool không thấy trong menu
→ Đợi Unity compile xong (check thanh progress dưới)

### Button không đổi màu
→ Check Inspector:
- Normal Speed Color = White
- Slow Speed Color = Yellow
- Fast Speed Color = Green

### Click không có gì xảy ra
→ Check Console:
- Phải thấy log: "[GameSpeedToggle] Tốc độ game: X.Xx"
- Nếu có lỗi NullReference → Button chưa được assign

### Text không cập nhật
→ Check Inspector:
- Speed Text phải được assign
- Hoặc Button phải có TextMeshProUGUI component con

### Muốn quay lại hệ thống 3 buttons cũ
→ Dễ dàng:
1. Xóa GameSpeedToggle component
2. Add lại GameSpeedController component
3. Tạo 3 buttons và assign

---

## 💡 PRO TIPS:

1. **Position button:**
   - Tool đặt ở góc trên phải
   - Có thể kéo đến vị trí khác tùy thích
   
2. **Size button:**
   - Default: 100x50
   - Điều chỉnh trong Rect Transform nếu cần

3. **Font size:**
   - Default: 24
   - Tăng lên nếu khó đọc

4. **Animation:**
   - Có thể thêm Animation khi click (tùy chọn)
   - Hoặc Sound effect khi chuyển tốc độ

5. **Hotkey:**
   - Nếu muốn, có thể thêm phím tắt (VD: Space bar)
   - Thêm code trong `Update()`:
     ```csharp
     if (Input.GetKeyDown(KeyCode.Space))
     {
         CycleSpeed();
     }
     ```

---

## ✅ CHECKLIST:

```
□ Unity đã compile xong
□ Đã chạy: Tools → Setup Map 2 Speed Toggle 🔄
□ Tool báo "Success! 🎉"
□ Scene đã save
□ Test Play mode
□ Click button → Tốc độ thay đổi ✅
□ Màu sắc thay đổi ✅
□ Text cập nhật ✅
□ Cycle lặp lại (2x → 0.5x → 1x → 2x) ✅
```

---

## 🎮 QUICK TEST:

```
1. Play Map 2 ▶️
2. Tìm button (hiện "1.0x", màu trắng)
3. Click 1 lần → "2.0x", màu xanh, enemies nhanh
4. Click 1 lần → "0.5x", màu vàng, enemies chậm
5. Click 1 lần → "1.0x", màu trắng, bình thường
→ ✅ SUCCESS!
```

---

## 🚀 BẮT ĐẦU NGAY:

```
Tools → Setup Map 2 Speed Toggle (1 Button) 🔄
```

**Chúc bạn thành công! 🎉**
