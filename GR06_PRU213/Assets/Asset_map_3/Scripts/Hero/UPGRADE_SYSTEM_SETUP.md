# 🎮 HƯỚNG DẪN SETUP HỆ THỐNG NÂNG CẤP TƯỚNG - MAP 3

## ✅ ĐÃ HOÀN THÀNH

### 1. **Code Implementation**
- ✅ Đã thêm hệ thống level và stats vào `Hero_Lv3.cs`
- ✅ Đã thêm multipliers nâng cấp vào `HeroData_Lv3.cs`
- ✅ Đã tạo `HeroUpgradePanel_Lv3.cs` để quản lý UI
- ✅ Đã cập nhật `Platform_Lv3.cs` để mở panel khi click tướng

### 2. **Tính Năng**
- ✅ Nâng cấp tướng (tối đa 3 level)
- ✅ Hiển thị stats hiện tại vs stats sau nâng cấp
- ✅ Bán tướng với hoàn tiền 70%
- ✅ Kiểm tra đủ vàng trước khi nâng cấp
- ✅ Tính giá nâng cấp tăng theo level

---

## 🔧 CÒN PHẢI LÀM - SETUP UI TRONG UNITY

### BƯỚC 1: Tạo Upgrade Panel UI

1. **Trong Scene Map 3:**
   - Right-click trong Canvas → UI → Panel
   - Đổi tên thành `HeroUpgradePanel`

2. **Setup Panel:**
   - Đặt Anchor: Center
   - Width: 400, Height: 500
   - Background: Màu tối (alpha 0.9)

### BƯỚC 2: Thêm các UI Elements

**Trong HeroUpgradePanel, tạo:**

1. **Hero Name Text (TMP)**
   - Tên: `TxtHeroName`
   - Font Size: 28
   - Alignment: Center Top
   - Text: "Hero Name"

2. **Level Text (TMP)**
   - Tên: `TxtLevel`
   - Font Size: 22
   - Text: "Level 1/3"

3. **Current Stats Text (TMP)**
   - Tên: `TxtCurrentStats`
   - Font Size: 18
   - Alignment: Left
   - Text mẫu:
     ```
     Current Stats:
     Damage: 35
     Range: 3.0
     Attack Speed: 1.0/s
     ```

4. **Next Stats Text (TMP)**
   - Tên: `TxtNextStats`
   - Font Size: 18
   - Color: Green
   - Text mẫu:
     ```
     Next Level:
     Damage: 42 (+7)
     Range: 3.3 (+0.3)
     Attack Speed: 1.18/s (+0.18)
     ```

5. **Upgrade Button**
   - Tên: `BtnUpgrade`
   - Width: 180, Height: 50
   - Text: "Upgrade (50 Gold)"
   - Color: Green

6. **Sell Button**
   - Tên: `BtnSell`
   - Width: 180, Height: 50
   - Text: "Sell (70 Gold)"
   - Color: Red

7. **Close Button (X)**
   - Tên: `BtnClose`
   - Width: 40, Height: 40
   - Position: Top-Right corner
   - Text: "X"

### BƯỚC 3: Gán References

1. **Chọn HeroUpgradePanel GameObject**
2. **Add Component → HeroUpgradePanel_Lv3**
3. **Trong Inspector, kéo thả:**
   - Panel Object → `HeroUpgradePanel` (chính nó)
   - Hero Name Text → `TxtHeroName`
   - Level Text → `TxtLevel`
   - Current Stats Text → `TxtCurrentStats`
   - Next Stats Text → `TxtNextStats`
   - Upgrade Button → `BtnUpgrade`
   - Sell Button → `BtnSell`
   - Close Button → `BtnClose`
   - Upgrade Cost Text → Text con của `BtnUpgrade`
   - Sell Value Text → Text con của `BtnSell`

4. **Điều chỉnh Sell Refund Percentage:**
   - Mặc định: 0.7 (70%)
   - Có thể thay đổi tùy ý

### BƯỚC 4: Gán Panel vào Platforms

**QUAN TRỌNG:** Mỗi Platform cần biết UpgradePanel ở đâu!

**CÁCH 1: Gán thủ công (Đơn giản)**
1. Chọn tất cả Platform trong Scene
2. Trong Inspector → Platform_Lv3 component
3. Kéo `HeroUpgradePanel` vào field `Upgrade Panel`

**CÁCH 2: Dùng Script tự động gán**
```csharp
// Thêm vào GameManager_Lv3 hoặc script khác
void Start()
{
    HeroUpgradePanel_Lv3 upgradePanel = FindObjectOfType<HeroUpgradePanel_Lv3>();
    Platform_Lv3[] platforms = FindObjectsOfType<Platform_Lv3>();
    
    foreach (var platform in platforms)
    {
        platform.upgradePanel = upgradePanel; // Cần public field
    }
}
```

### BƯỚC 5: Cập nhật ScriptableObjects

**Cho mỗi Hero (Lyra, Tethys, Archer, Lorian):**

1. Mở file asset trong Inspector
2. Điều chỉnh các giá trị:
   - `Max Level`: 3
   - `Damage Upgrade Multiplier`: 0.2 (tăng 20%/level)
   - `Range Upgrade Multiplier`: 0.1 (tăng 10%/level)
   - `Shoot Speed Upgrade Multiplier`: 0.15 (giảm 15% interval/level)

**Ví dụ đề xuất cho từng tướng:**

| Tướng | Damage/Lv | Range/Lv | Speed/Lv |
|-------|-----------|----------|----------|
| Lyra | 0.20 | 0.10 | 0.15 |
| Tethys | 0.25 | 0.12 | 0.18 |
| Archer | 0.22 | 0.08 | 0.15 |
| Lorian | 0.20 | 0.10 | 0.20 |

---

## 🎯 CÁCH SỬ DỤNG

### Trong Game:
1. **Đặt tướng**: Click vào platform trống → Chọn tướng
2. **Nâng cấp**: Click vào tướng đã đặt → Click "Upgrade"
3. **Bán tướng**: Trong panel → Click "Sell"

### Stats sau nâng cấp:
- **Level 1 → 2**: Stats × (1 + multiplier)
- **Level 2 → 3**: Stats × (1 + multiplier × 2)

**Ví dụ với Tethys (Damage 35, multiplier 0.25):**
- Level 1: 35 damage
- Level 2: 35 × 1.25 = 43.75 damage
- Level 3: 35 × 1.5 = 52.5 damage

### Giá nâng cấp:
- **Level 1 → 2**: upgradeCost × 1 = 50 vàng
- **Level 2 → 3**: upgradeCost × 2 = 100 vàng

### Giá bán:
- Hoàn 70% tổng số tiền đã đầu tư (giá mua + tổng tiền nâng cấp)
- **Ví dụ**: Tethys Level 3
  - Mua: 125
  - Upgrade L2: 50
  - Upgrade L3: 100
  - **Tổng**: 275 → Bán được: 192 vàng

---

## 🐛 TROUBLESHOOTING

### Lỗi: "UpgradePanel chưa được gán"
- **Nguyên nhân**: Chưa kéo HeroUpgradePanel vào Platform
- **Giải pháp**: Làm theo BƯỚC 4

### Lỗi: "Không đủ vàng để nâng cấp"
- **Nguyên nhân**: Gold không đủ
- **Giải pháp**: Button sẽ tự động disable, màu đỏ

### Panel không hiện
- **Kiểm tra**: Panel Object có được Active ban đầu không
- **Giải pháp**: Để inactive ban đầu, script sẽ tự bật

### Click tướng không mở panel
- **Kiểm tra**: Layer của Platform có đúng không
- **Kiểm tra**: upgradePanel đã gán chưa

---

## 🎨 TÙY CHỈNH NÂNG CAO

### Thay đổi tỷ lệ hoàn tiền khi bán:
```csharp
// Trong HeroUpgradePanel_Lv3
[SerializeField] private float sellRefundPercentage = 0.8f; // 80%
```

### Thêm visual effects khi upgrade:
```csharp
// Trong Hero_Lv3.Upgrade()
public void Upgrade()
{
    // ... code hiện tại ...
    
    // Thêm particle effect
    if (upgradeParticle != null)
        Instantiate(upgradeParticle, transform.position, Quaternion.identity);
    
    // Phát âm thanh
    if (upgradeSound != null && audioSource != null)
        audioSource.PlayOneShot(upgradeSound);
}
```

### Thay đổi màu tướng theo level:
```csharp
// Trong Hero_Lv3.Upgrade()
private void UpdateVisual()
{
    SpriteRenderer sr = GetComponent<SpriteRenderer>();
    if (sr != null)
    {
        switch (_currentLevel)
        {
            case 2: sr.color = new Color(1f, 1f, 0.5f); break; // Vàng
            case 3: sr.color = new Color(1f, 0.5f, 1f); break; // Tím
        }
    }
}
```

---

## ✨ TÍNH NĂNG BỔ SUNG (OPTIONAL)

### 1. Hiển thị Level trên tướng
- Thêm TextMeshPro trên prefab tướng
- Update text khi upgrade

### 2. Animation khi upgrade
- Thêm particle system
- Scale animation

### 3. Confirm dialog khi bán
- Tránh bán nhầm
- "Are you sure?"

### 4. Keyboard shortcuts
- E: Upgrade
- S: Sell
- ESC: Close panel

---

## 📝 CHECKLIST SETUP

- [ ] Tạo UI Panel
- [ ] Thêm tất cả UI elements
- [ ] Gán HeroUpgradePanel_Lv3 script
- [ ] Kéo thả tất cả references
- [ ] Gán panel vào tất cả Platforms
- [ ] Cập nhật ScriptableObjects
- [ ] Test trong game
- [ ] Kiểm tra vàng đủ/không đủ
- [ ] Test bán tướng
- [ ] Test nâng cấp đến max level

---

**🎉 Chúc bạn setup thành công!**

Nếu gặp vấn đề, kiểm tra Console để xem lỗi gì.
