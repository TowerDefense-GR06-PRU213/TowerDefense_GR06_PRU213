# HƯỚNG DẪN FIX UI MAP 2 - HIỂN THỊ TÊN HERO VÀ GIÁ TIỀN

## ❌ VẤN ĐỀ:
- Bảng hero selection bị trắng, không thấy tên hero và giá tiền
- Nút X (Close) không hiển thị

## ✅ ĐÃ SỬA TRONG CODE:
1. **Tự động tắt các Image trống** đang che lấp text
2. **Tự động hiển thị nút Close** (màu đỏ với chữ X trắng)
3. **Tự động populate tên hero và giá tiền** lên UI

## 📋 CÁCH SETUP TRONG UNITY:

### Bước 1: Mở Scene Map 2
1. Mở Unity
2. Load scene `Game_Map2`

### Bước 2: Tìm UI_mua_tuong trong Scene
1. Trong **Hierarchy**, tìm object `UI_mua_tuong` hoặc `HeroSelectionUI`
2. Click vào object đó

### Bước 3: Gán các Text components
Trong **Inspector**, tìm component **HeroSelectionUI Script**:

#### A. Gán Hero Names (Array String):
```
Hero Names:
  Size: 4
  Element 0: Archer
  Element 1: Lyra  
  Element 2: Selim
  Element 3: Tethys
```

#### B. Gán Hero Name Texts (Array TextMeshProUGUI):
Trong **Hierarchy**, mở rộng UI_mua_tuong và tìm các Text object:
- `ArcherText` → Kéo vào Element 0
- `LyraText` → Kéo vào Element 1
- `SelimText` → Kéo vào Element 2
- `TethysText` → Kéo vào Element 3

```
Hero Name Texts:
  Size: 4
  Element 0: [ArcherText]
  Element 1: [LyraText]
  Element 2: [SelimText]
  Element 3: [TethysText]
```

#### C. Gán Hero Cost Texts (Array TextMeshProUGUI):
Tìm các "BuyText" object trong mỗi hero card:
- Card Archer → BuyText → Kéo vào Element 0
- Card Lyra → BuyText → Kéo vào Element 1
- Card Selim → BuyText → Kéo vào Element 2
- Card Tethys → BuyText → Kéo vào Element 3

```
Hero Cost Texts:
  Size: 4
  Element 0: [BuyText của Archer]
  Element 1: [BuyText của Lyra]
  Element 2: [BuyText của Selim]
  Element 3: [BuyText của Tethys]
```

#### D. Kiểm tra Hero Costs (đã có sẵn):
```
Hero Costs:
  Size: 4
  Element 0: 100
  Element 1: 120
  Element 2: 180
  Element 3: 180
```

#### E. Kiểm tra Close Button:
Đảm bảo field **Close Button** đã được gán:
- Nếu chưa có, kéo object `Closebutotn` hoặc `CloseButton` vào đây

### Bước 4: Save và Test
1. Nhấn **Ctrl + S** để save
2. Chạy game Map 2
3. Click vào platform → Sẽ thấy:
   - ✅ Tên hero (màu đen, in đậm)
   - ✅ Giá tiền (màu vàng, in đậm)
   - ✅ Nút X màu đỏ ở góc trên phải

## 🔧 NẾU VẪN LỖI:

### Lỗi: Vẫn không thấy text
- Kiểm tra xem các Text object có component **TextMeshPro - Text (UI)** không
- Đảm bảo Font Asset không bị None

### Lỗi: Nút X vẫn không hiện
- Kiểm tra trong Console log xem có dòng "[HeroSelectionUI] Đã sửa close button" không
- Nếu không có → Close Button chưa được gán

### Lỗi: Khung trắng vẫn che
- Xem Console log, sẽ có dòng "[HeroSelectionUI] Đã tắt Image trống"
- Nếu không có → Có thể Image object có tên khác "Image"

## 📝 GHI CHÚ:
- Code đã được sửa trong file: `Assets/Asset_map_2/Script/HeroSelectionUI.cs`
- Tất cả fix đều chạy tự động khi game start
- Chỉ cần gán đúng các Text components trong Inspector là xong!
