# ⚡ HƯỚNG DẪN NHANH - HỆ THỐNG NÂNG CẤP TƯỚNG

## 🚀 SETUP NHANH TRONG 5 PHÚT

### 1️⃣ TẠO UI PANEL (2 phút)

**Trong Scene Map 3:**

```
Canvas
└── HeroUpgradePanel
    ├── TxtHeroName (TextMeshPro)
    ├── TxtLevel (TextMeshPro)
    ├── TxtCurrentStats (TextMeshPro)
    ├── TxtNextStats (TextMeshPro)
    ├── BtnUpgrade (Button)
    │   └── Text (TextMeshPro) ← upgradeCostText
    ├── BtnSell (Button)
    │   └── Text (TextMeshPro) ← sellValueText
    └── BtnClose (Button)
```

**Tips:**
- Copy panel từ map khác nếu có
- Hoặc dùng Unity UI → Panel → Customize

---

### 2️⃣ GÁN SCRIPT (1 phút)

1. Chọn `HeroUpgradePanel`
2. Add Component → `HeroUpgradePanel_Lv3`
3. Kéo thả TẤT CẢ UI elements vào đúng slot
4. Set `Sell Refund Percentage` = 0.7

---

### 3️⃣ KẾT NỐI VỚI PLATFORMS (1 phút)

**CÁCH DỄ NHẤT:**

1. Tạo Empty GameObject tên `GameManager_Lv3` (nếu chưa có)
2. Add Component → `UpgradePanelAutoAssigner_Lv3`
3. Kéo `HeroUpgradePanel` vào slot `Upgrade Panel`
4. Check `Auto Assign On Start`
5. ✅ **XONG!** Script sẽ tự gán cho tất cả Platforms khi chạy

**CÁCH THỦ CÔNG (nếu cần):**
- Chọn tất cả Platform
- Kéo `HeroUpgradePanel` vào `Upgrade Panel` field

---

### 4️⃣ CẬP NHẬT HERO DATA (1 phút)

**Mở từng Hero ScriptableObject:**
- `Lyra.asset`
- `Tethys.asset`
- `Archer.asset`
- `Lorian.asset`

**Set các giá trị:**
```
Max Level: 3
Damage Upgrade Multiplier: 0.2
Range Upgrade Multiplier: 0.1
Shoot Speed Upgrade Multiplier: 0.15
```

---

## ✅ TEST

1. Play Scene
2. Đặt tướng
3. Click vào tướng → Panel xuất hiện ✅
4. Click Upgrade → Tướng mạnh hơn ✅
5. Click Sell → Nhận vàng ✅

---

## 📊 THÔNG SỐ ĐỀ XUẤT

### Giá Nâng Cấp:
```
Level 1 → 2: 50 vàng
Level 2 → 3: 100 vàng
```

### Stats Tăng:
```
Damage: +20%/level
Range: +10%/level
Attack Speed: +15%/level
```

### Ví Dụ: Tethys
```
Level 1: 35 dmg, 3.0 range, 1.0 atk/s
Level 2: 42 dmg, 3.3 range, 1.18 atk/s (+50 gold)
Level 3: 49 dmg, 3.6 range, 1.35 atk/s (+100 gold)

Tổng đầu tư: 125 + 50 + 100 = 275 vàng
Bán được: 275 × 0.7 = 192 vàng
```

---

## 🎮 CÁCH CHƠI

| Hành Động | Cách Thực Hiện |
|-----------|----------------|
| **Đặt tướng** | Click platform trống |
| **Xem thông tin** | Click tướng đã đặt |
| **Nâng cấp** | Trong panel → Click "Upgrade" |
| **Bán tướng** | Trong panel → Click "Sell" |
| **Đóng panel** | Click nút "X" hoặc ngoài panel |

---

## 🐛 SỬA LỖI NHANH

| Lỗi | Giải Pháp |
|-----|-----------|
| Panel không hiện | Kiểm tra upgradePanel đã gán trong Platform chưa |
| Console báo lỗi null | Kiểm tra TẤT CẢ UI elements đã gán chưa |
| Không nâng cấp được | Kiểm tra đủ vàng, chưa max level |
| Giá nâng cấp = 0 | Kiểm tra upgradeCost trong HeroData |

---

## 🎨 BONUS: TỐI ƯU HÓA

### Thêm Hotkeys:
```csharp
// Trong HeroUpgradePanel_Lv3.Update()
if (Input.GetKeyDown(KeyCode.E)) OnUpgradeClicked();
if (Input.GetKeyDown(KeyCode.S)) OnSellClicked();
if (Input.GetKeyDown(KeyCode.Escape)) OnCloseClicked();
```

### Thêm Confirm Bán:
```csharp
// Trước OnSellClicked
if (!EditorUtility.DisplayDialog("Confirm", 
    "Bán tướng này?", "Yes", "No")) return;
```

### Hiển thị Level trên Tướng:
```csharp
// Thêm vào Hero prefab: TextMeshPro
// Update trong Hero_Lv3.Upgrade():
levelText.text = $"Lv{_currentLevel}";
```

---

**✨ Hoàn thành! Giờ bạn đã có hệ thống nâng cấp tướng hoàn chỉnh!**
