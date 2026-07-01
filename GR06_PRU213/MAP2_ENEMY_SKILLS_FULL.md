# 🎮 DANH SÁCH ĐẦY ĐỦ QUÁI VÀ KỸ NĂNG - MAP 2 (SA MẠC)

## 📋 TỔNG QUAN:
Map 2 có **5 loại quái**, trong đó **3 loại có kỹ năng đặc biệt**!

---

## 1️⃣ **ORC** (Quái thường)
📁 `Assets/Asset_map_2/Prefab/orc/Orc.prefab`

### **Thông số:**
- Loại: Quái cơ bản
- **KHÔNG CÓ KỸ NĂNG ĐẶC BIỆT**
- Chỉ đi theo đường và tấn công cổng

### **Đặc điểm:**
- HP: Thấp
- Speed: Trung bình
- Vai trò: Cannon fodder (lính mồi)

---

## 2️⃣ **ZOMBIE** (Quái thường)
📁 `Assets/Asset_map_2/Prefab/Zombie/Zombie Village.prefab`

### **Thông số:**
- Loại: Quái cơ bản
- **KHÔNG CÓ KỸ NĂNG ĐẶC BIỆT**
- Chỉ đi theo đường và tấn công cổng

### **Đặc điểm:**
- HP: Trung bình
- Speed: Chậm
- Vai trò: Tanky fodder (lính mồi có máu)

---

## 3️⃣ **GOLEM** ⚡ (CÓ SKILL)
📁 `Assets/Asset_map_2/Prefab/Golem/Golem.prefab`
📜 `GolemSkill.cs`

### **🔥 KỸ NĂNG: TĂNG TỐC KHI BỊ ĐÁNH**

#### **Cơ chế:**
- **Trigger**: Kích hoạt **KHI BỊ ĐÁNH LẦN ĐẦU TIÊN**
- **Hiệu ứng**: Tăng tốc độ bản thân **x1.5** (tăng 50%)
- **Thời gian**: 2 giây
- **Hồi chiêu**: 6 giây (sau khi hết buff)
- **VFX**: Hiệu ứng lửa phía sau lưng Golem

#### **Chiến thuật:**
- Đừng để Golem bị đánh → nó sẽ CHẠY NHANH
- Nếu bắn thì bắn chết luôn trong 2 giây buff
- Ưu tiên target khác nếu Golem đang buff

#### **Code logic:**
```csharp
// Lần đầu bị đánh → kích hoạt
OnDamaged → KichHoatTangToc()
speed = tocDoGoc * 1.5f
// Sau 2s → hết buff
speed = tocDoGoc
```

---

## 4️⃣ **TINY GOLEM** 🔄 (CÓ SKILL - NGUY HIỂM!)
📁 `Assets/Asset_map_2/Prefab/Tiny Golem/Tiny Golem.prefab`
📜 `TinyGolemSkill.cs`

### **🌟 KỸ NĂNG: BUFF TỐC ĐỘ AOE (SUPPORT)**

#### **Cơ chế:**
- **Type**: AOE Buff (vùng tròn màu vàng/cam)
- **Auto-cast**: Tự động thả **MỖI 6 GIÂY**
- **Phạm vi**: 4 units
- **Target**: TẤT CẢ quái đồng minh trong vùng
- **Buff**: Tăng tốc độ trong 4 giây

#### **Buff tốc độ:**
| HP Tiny Golem | Buff % | Hệ số |
|---------------|--------|-------|
| > 50% HP | +40% | x1.4 |
| ≤ 50% HP | +65% | x1.65 |

#### **Hiệu ứng:**
- ✨ Vùng tròn ánh sáng lan tỏa
- 🔊 Âm thanh roar/thump
- 💨 Quái trong vùng chạy nhanh x1.4 ~ x1.65

#### **Chiến thuật:**
- **ƯU TIÊN SÁT TINY GOLEM TRƯỚC!**
- Đây là quái support nguy hiểm nhất
- Nếu để sống → toàn bộ wave chạy cực nhanh
- Buff stack nếu có nhiều Tiny Golem!

#### **Code logic:**
```csharp
// Mỗi 6 giây
Update() → demHoiChieu -= Time.deltaTime
if (demHoiChieu <= 0) → BuffDongMinh()

// Tìm quái xung quanh
Physics2D.OverlapCircleAll(position, 4f)
// Buff tốc độ 4 giây
move.speed *= (HP > 50% ? 1.4f : 1.65f)
```

---

## 5️⃣ **VALKYRIE** 🛡️ (CÓ SKILL - TANK)
📁 `Assets/Asset_map_2/Prefab/Valkyrie/Valkyrie.prefab`
📜 `ValkyrieSkill.cs`

### **🛡️ KỸ NĂNG: KHIÊN GIẢM SÁT THƯƠNG**

#### **Cơ chế:**
- **Trigger**: Kích hoạt **KHI BỊ ĐÁNH**
- **Hiệu ứng**: Giảm sát thương nhận vào **20%** (chỉ nhận 80% damage)
- **Thời gian**: 2.5 giây
- **Hồi chiêu**: 6 giây (sau khi hết khiên)
- **VFX**: Cái khiên phóng to nhấp nháy

#### **Ví dụ:**
- Tháp bắn 100 damage
- Valkyrie kích hoạt khiên
- Chỉ nhận **80 damage** (giảm 20)
- Khiên tồn tại 2.5 giây
- Hồi 6 giây mới dùng lại

#### **Chiến thuật:**
- Valkyrie rất tanky khi có khiên
- Đợi khiên hết (2.5s) rồi tập trung bắn
- Hoặc dùng hero damage cao để phá khiên nhanh
- Trong 6s hồi chiêu = thời điểm dễ giết nhất

#### **Code logic:**
```csharp
// Khi bị đánh
OnTakeDamage(int damage)
if (!dangHoiChieu) {
    // Kích hoạt khiên
    dangGiamSatThuong = true
    damage *= 0.8f (giảm 20%)
}

// Sau 2.5s → hết khiên → hồi chiêu 6s
```

---

## 📊 BẢNG SO SÁNH KỸ NĂNG:

| Enemy | Skill Type | Trigger | Cooldown | Đặc điểm |
|-------|-----------|---------|----------|----------|
| **Orc** | ❌ None | - | - | Lính mồi |
| **Zombie** | ❌ None | - | - | Lính mồi tanky |
| **Golem** | ⚡ Self Buff | Bị đánh lần đầu | 6s | Tăng tốc x1.5 |
| **Tiny Golem** | 🔄 AOE Support | Auto 6s | 6s | Buff đồng minh x1.4~1.65 |
| **Valkyrie** | 🛡️ Defense | Bị đánh | 6s | Giảm 20% damage |

---

## ⚠️ ĐỘ NGUY HIỂM:

### **🔴 Cực kỳ nguy hiểm:**
1. **Tiny Golem** - Buff toàn đội, phải giết trước!

### **🟠 Nguy hiểm:**
2. **Valkyrie** - Tank cứng, khó giết
3. **Golem** - Tăng tốc bất ngờ

### **🟢 Ít nguy hiểm:**
4. **Zombie** - Chậm, dễ bắn
5. **Orc** - Yếu nhất

---

## 🎯 CHIẾN THUẬT TỔNG HỢP:

### **Thứ tự ưu tiên tiêu diệt:**
1. **TINY GOLEM** (buff cả đám)
2. **GOLEM** (tốc độ cao)
3. **VALKYRIE** (tanky)
4. **ZOMBIE** (chậm)
5. **ORC** (yếu)

### **Lưu ý:**
- **Tiny Golem** là threat #1 - giết càng sớm càng tốt
- Đợi **Valkyrie** hết khiên rồi focus fire
- **Golem** khi buff thì tránh bắn, đợi hết buff
- Dùng slow/stun để counter buff speed

---

## 📝 FILES SCRIPT:

```
Assets/Asset_map_2/Prefab/
├── orc/Orc.prefab (no skill)
├── Zombie/Zombie Village.prefab (no skill)
├── Golem/
│   ├── Golem.prefab
│   └── GolemSkill.cs ⚡
├── Tiny Golem/
│   ├── Tiny Golem.prefab
│   └── TinyGolemSkill.cs 🔄
└── Valkyrie/
    ├── Valkyrie.prefab
    └── ValkyrieSkill.cs 🛡️
```

---

## 🎮 KẾT LUẬN:

Map 2 có **hệ thống skill phức tạp hơn Map 1**:
- Map 1: Skill tập trung vào boss (phân chia, né đòn)
- **Map 2: Skill tactical (buff đội, defense, mobility)**

Tiny Golem là **game changer** - nếu không giết nó trước, toàn bộ wave sẽ cực kỳ nhanh! 🔥
