# 🛠️ EDITOR TOOLS - HƯỚNG DẪN SỬ DỤNG

## 🚀 SETUP NHANH NHẤT (1 CLICK!)

### **Mở Scene Map 3, sau đó:**

1. Vào menu Unity: **`Tools → Map 3 → Complete Setup (All-in-One)`**
2. Click **"Yes, Do It!"**
3. ✅ **XONG!** Tất cả đã được setup tự động!

---

## 📋 CÁC CÔNG CỤ EDITOR

### **Menu: Tools → Map 3**

#### 1️⃣ **Complete Setup (All-in-One)** ⭐ RECOMMENDED
**Làm tất cả trong 1 click:**
- ✅ Cập nhật tất cả Hero ScriptableObjects
- ✅ Tạo Upgrade Panel UI
- ✅ Gán Panel cho tất cả Platforms
- ✅ Hiển thị Preview Stats

**Khi nào dùng:** Lần đầu setup hoặc muốn reset toàn bộ

---

#### 2️⃣ **Create Upgrade Panel UI**
**Tạo HeroUpgradePanel với tất cả UI elements:**
- Panel chính (400×500)
- 7 UI components (texts + buttons)
- Tự động gán script và references

**Khi nào dùng:** Chỉ cần tạo lại Panel

---

#### 3️⃣ **Update All Hero Data (Add Upgrade Stats)**
**Cập nhật ScriptableObjects:**
- Set maxLevel = 3
- Set damage/range/speed multipliers
- Giá trị tối ưu cho từng hero

**Khi nào dùng:** Cập nhật stats mà không động đến UI

**Giá trị được set:**
```
Lyra:
  - Damage Multiplier: 0.20 (+20%/lv)
  - Range Multiplier: 0.10 (+10%/lv)
  - Speed Multiplier: 0.15 (+15%/lv)

Tethys: (Buff mạnh hơn)
  - Damage Multiplier: 0.25 (+25%/lv)
  - Range Multiplier: 0.12 (+12%/lv)
  - Speed Multiplier: 0.18 (+18%/lv)

Archer:
  - Damage Multiplier: 0.20
  - Range Multiplier: 0.10
  - Speed Multiplier: 0.15

Lorian: (Bắn nhanh)
  - Damage Multiplier: 0.20
  - Range Multiplier: 0.10
  - Speed Multiplier: 0.20 (+20%/lv)
```

---

#### 4️⃣ **Assign Panel to All Platforms**
**Kết nối Panel với Platforms:**
- Tìm HeroUpgradePanel_Lv3
- Tìm tất cả Platform_Lv3
- Gán tự động qua SerializedObject

**Khi nào dùng:** Đã có Panel, chỉ cần kết nối

---

#### 5️⃣ **Show Hero Stats Preview**
**Hiển thị stats của tất cả heroes:**
- Level 1, 2, 3 stats
- DPS calculations
- Ice hero bonus

**Khi nào dùng:** Kiểm tra stats sau khi update

---

#### 6️⃣ **Verify Setup**
**Kiểm tra toàn bộ setup:**
- ✅ Hero Data có đúng không
- ✅ Panel có trong Scene không
- ✅ Platforms đã connect chưa
- ✅ Scripts có đầy đủ không

**Khi nào dùng:** Sau khi setup, trước khi test game

---

## 📊 WORKFLOW ĐỀ XUẤT

### **Lần Đầu Setup:**
```
1. Mở Scene Map 3
2. Tools → Map 3 → Complete Setup
3. Done! ✅
```

### **Chỉ Update Stats:**
```
1. Tools → Map 3 → Update All Hero Data
2. Tools → Map 3 → Show Hero Stats Preview
```

### **Chỉ Tạo Lại UI:**
```
1. Tools → Map 3 → Create Upgrade Panel UI
2. Tools → Map 3 → Assign Panel to All Platforms
```

### **Kiểm Tra Setup:**
```
Tools → Map 3 → Verify Setup
```

---

## 🎯 OUTPUT MẪU

### **Sau khi chạy Complete Setup:**

**Console sẽ hiển thị:**
```
📝 Bước 1: Cập nhật Hero Data...
✅ Đã cập nhật: Lyra
✅ Đã cập nhật: Tethys
✅ Đã cập nhật: Archer
✅ Đã cập nhật: Lorian
✅ Đã cập nhật 4 Hero Data!

🎨 Bước 2: Tạo Upgrade Panel...
✅ Đã tạo HeroUpgradePanel thành công!
✅ Đã gán tất cả references tự động!

🔗 Bước 3: Gán Panel cho Platforms...
✅ Đã gán UpgradePanel cho 12 Platform(s)!

📊 Bước 4: Hiển thị Stats Preview...
📊 HERO STATS PREVIEW - ALL LEVELS

🎯 Lyra
   Cost: 100 gold | Upgrade Cost: 50/level
   Base: 30 dmg | 2.5 range | 0.67 atk/s
   Lv2: 36.0 dmg | 2.8 range | 0.78 atk/s
   Lv3: 42.0 dmg | 3.0 range | 0.90 atk/s

🎯 Tethys
   Cost: 125 gold | Upgrade Cost: 50/level
   Base: 35 dmg | 3.0 range | 1.00 atk/s
   Lv2: 43.8 dmg | 3.4 range | 1.22 atk/s
   Lv3: 52.5 dmg | 3.8 range | 1.47 atk/s
   💎 Vs Ice: 68.3 dmg | 1.84 atk/s | DPS: 125.5

... (tiếp tục)

🎉 HOÀN THÀNH TẤT CẢ!
```

---

## 🐛 TROUBLESHOOTING

### **Lỗi: "Không tìm thấy Canvas"**
**Giải pháp:** Tạo Canvas trong Scene (GameObject → UI → Canvas)

### **Lỗi: "HeroUpgradePanel đã tồn tại"**
**Giải pháp:** Click "Yes, Overwrite" để tạo lại

### **Lỗi: "Không tìm thấy Platform_Lv3"**
**Giải pháp:** Đảm bảo đang mở đúng Scene Map 3

### **Panel không hiện trong game**
**Kiểm tra:**
1. Panel có component HeroUpgradePanel_Lv3?
2. Panel đang inactive ban đầu? (đúng!)
3. Platforms đã gán upgradePanel chưa?

**Chạy:** Tools → Map 3 → Verify Setup

---

## 💡 TIPS & TRICKS

### **Tùy Chỉnh Stats:**
Sau khi chạy tool, bạn vẫn có thể:
- Mở từng Hero asset
- Điều chỉnh multipliers theo ý muốn
- Tool chỉ set giá trị mặc định tối ưu

### **Tùy Chỉnh UI:**
Sau khi tool tạo panel:
- Có thể đổi màu, size, position
- Thêm animations
- Thêm backgrounds/borders

### **Backup:**
Trước khi chạy Complete Setup:
- Backup Scene (Ctrl+D)
- Hoặc dùng Git commit

### **Multi-Scene:**
Nếu có nhiều scenes Map 3:
- Mở từng scene
- Chạy Assign Panel to All Platforms

---

## 📝 CHECKLIST

Sau khi chạy Complete Setup:

- [ ] Console không có lỗi đỏ
- [ ] HeroUpgradePanel xuất hiện trong Canvas
- [ ] Chạy Verify Setup → "All Good!"
- [ ] Play game → Click platform trống → Đặt tướng
- [ ] Click tướng → Panel xuất hiện
- [ ] Upgrade hoạt động
- [ ] Sell hoạt động
- [ ] Stats hiển thị đúng

---

## 🎓 HỌC THÊM

### **Code các tools:**
- `UpgradePanelCreator.cs` - Tạo UI
- `HeroDataUpdater.cs` - Update ScriptableObjects
- `PlatformSetup.cs` - Kết nối và verify

### **Mở rộng:**
Bạn có thể tạo thêm tools tương tự cho:
- Map 1, 2, 4, 5
- Auto-create Enemy prefabs
- Batch update animations

---

**🎉 Chúc bạn sử dụng tools hiệu quả!**

*P.S: Tất cả các tools đều an toàn, có confirm dialog trước khi thực hiện thay đổi lớn.*
