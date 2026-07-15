# 🔄 Kế Hoạch Kết Hợp Map 4 & Map 5 từ FA25 vào GR06

## 📋 TỔNG QUAN

**Mục tiêu:** Cập nhật **quái** và **map assets** cho Map 4 & 5 từ dự án FA25 vào GR06, nhưng **GIỮ NGUYÊN** logic game hiện tại.

**Các chức năng PHẢI GIỮ NGUYÊN:**
- ✅ Lưu tài nguyên giữa các lần chơi
- ✅ System Mission Complete
- ✅ Nút Fast-Forward (tua nhanh)
- ✅ Toàn bộ game mechanics (spawning, wave system, hero placement, etc.)

**Chỉ CẬP NHẬT:**
- 🎨 Enemy sprites/animations (quái)
- 🗺️ Map background (bản đồ)
- 🎵 Audio assets (nếu có)

---

## 🔍 BƯỚC 1: PHÂN TÍCH CẤU TRÚC

Trước khi merge, tôi cần kiểm tra xem FA25 có những gì khác so với GR06.

### Cần Làm:

**Option A: Copy FA25 vào workspace để tôi phân tích**
```powershell
# Chạy lệnh này trong terminal:
xcopy "C:\Users\THANH\Downloads\FA25_Gr7_Project_PRU213-main\FA25_Gr7_Project_PRU213-main" "c:\Users\THANH\Documents\GitHub\TowerDefense_GR06_PRU213\FA25_SOURCE" /E /I /H /Y
```

**Option B: Bạn cung cấp thông tin thủ công**

Hãy cho tôi biết trong FA25:

1. **Map 4 có những quái nào?**
   - Tên từng loại quái
   - Có skill đặc biệt gì không
   - File sprite/animation ở đâu

2. **Map 5 có những quái nào?**
   - Tên từng loại quái
   - Có skill đặc biệt gì không
   - File sprite/animation ở đâu

3. **Map assets khác biệt gì?**
   - Map background file name
   - Có thêm UI assets không
   - Có thêm music/sound không

4. **Kiến trúc code có khác không?**
   - Enemy script tên gì? (Enemy_map_4.cs hay tên khác?)
   - GameManager tên gì?
   - Có dùng Ability System không?

---

## 📊 SO SÁNH CẤU TRÚC HIỆN TẠI

### Cấu trúc GR06 - Map 4 (hiện tại):
```
Assets/Asset_map_4/
├── Animations_map_4/
│   ├── Enemies_map_4/     (animations quái)
│   ├── Hero_map_4/        (animations hero)
│   └── Effects_map_4/     (effects)
├── Art_map_4/
│   └── Map4_v2.png        (map hiện tại)
├── Asset_code/
│   ├── Enemy/             (sprites quái)
│   ├── Hero/              (sprites hero)
│   └── UI/                (UI sprites)
├── Scripts_map_4/
│   ├── Enemy/
│   │   └── Enemy_map_4.cs
│   ├── Hero/
│   │   └── Hero_map_4.cs
│   └── GameManager_map_4.cs
├── ScriptableObjects_map_4/
│   ├── Enemy/
│   ├── Hero/
│   └── Wave/
└── Prefabs_map_4/
    ├── Enemies/
    ├── Hero/
    └── UI/
```

### Cấu trúc GR06 - Map 5 (hiện tại):
```
Assets/Asset_map_5/
├── Art/
│   ├── Bongma/           (quái Bóng Ma)
│   ├── Bongmaacdoc/      (quái Bóng Ma Ác Độc)
│   ├── Xuong/            (quái Xương)
│   ├── Phuthuybongtoi/   (quái Phù Thủy)
│   ├── Chuatebongtoi/    (Boss Chúa Tể Bóng Tối)
│   ├── Hero/             (4 heroes)
│   └── map.png           (map background)
├── Scripts/
│   ├── Bongma.cs         (Enemy script với Ability System!)
│   ├── Xuong.cs
│   ├── GameManager.cs
│   ├── IAbility.cs
│   ├── DamageReductionAbility.cs
│   ├── EvasionAbility.cs
│   ├── HealAuraAbility.cs
│   ├── RageAbility.cs
│   └── SplittingAbility.cs
├── ScriptableObjects/
│   ├── Enemy/
│   ├── Hero/
│   └── Wave/
└── Prefabs/
    ├── Bongma.prefab
    ├── Xuong.prefab
    ├── ...
```

**⚠️ QUAN TRỌNG:** Map 5 có **Ability System** (IAbility interface), Map 4 thì KHÔNG!

---

## 🎯 3 PHƯƠNG ÁN MERGE

Sau khi phân tích FA25, tôi sẽ đề xuất 1 trong 3 option sau:

### 🟢 OPTION A: CHỈ THAY ASSETS (ĐƠN GIẢN NHẤT)

**Khi nào dùng:** FA25 chỉ khác về sprites/animations, còn code tương tự GR06

**Cách làm:**
1. Copy sprites quái mới từ FA25 → GR06
2. Copy map background mới
3. Cập nhật Prefabs để dùng sprites mới
4. **KHÔNG** động vào Scripts, ScriptableObjects

**Ưu điểm:**
- ✅ An toàn nhất
- ✅ Không risk phá logic cũ
- ✅ Nhanh (30 phút)

**Nhược điểm:**
- ❌ Không thêm được skill mới của quái (nếu FA25 có)

---

### 🟡 OPTION B: THAY ASSETS + THÊM ENEMY SKILLS (HYBRID)

**Khi nào dùng:** FA25 có thêm skills cho quái mà GR06 chưa có

**Cách làm:**
1. Copy assets như Option A
2. Phân tích skills mới trong FA25
3. **Viết thêm** skills vào Enemy_map_4.cs / Enemy_map_5.cs của GR06
4. Cập nhật EnemyData ScriptableObjects

**Ưu điểm:**
- ✅ Giữ nguyên architecture GR06
- ✅ Thêm được skills mới
- ✅ Tương thích với fast-forward button

**Nhược điểm:**
- ⚠️ Phải code thêm skills (1-2 giờ)
- ⚠️ Cần test kỹ

---

### 🔴 OPTION C: REPLACE TOÀN BỘ MAP (RỦI RO CAO)

**Khi nào dùng:** FA25 có architecture hoàn toàn khác và tốt hơn

**Cách làm:**
1. Backup Map 4 & 5 hiện tại
2. Xóa toàn bộ Asset_map_4 & Asset_map_5
3. Copy nguyên từ FA25
4. Migrate logic lưu tài nguyên + fast-forward vào FA25 code

**Ưu điểm:**
- ✅ Nhận được mọi thứ từ FA25

**Nhược điểm:**
- ❌ RỦI RO CAO - có thể phá game
- ❌ Mất thời gian (3-4 giờ)
- ❌ Phải test lại toàn bộ

**⛔ KHÔNG KHUYẾN KHÍCH** trừ khi FA25 quá vượt trội

---

## 🤔 TÔI NÊN CHỌN GÌ?

**Hỏi bản thân:**

1. **FA25 có quái với skill đặc biệt không?**
   - ❌ Không → Chọn **OPTION A**
   - ✅ Có → Chọn **OPTION B**

2. **FA25 có game mechanics khác biệt không?**
   - ❌ Không → Chọn **OPTION A** hoặc **B**
   - ✅ Có và tốt hơn → Chọn **OPTION C** (rủi ro)

3. **Bạn có thời gian bao nhiêu?**
   - 30 phút → **OPTION A**
   - 1-2 giờ → **OPTION B**
   - 3-4 giờ → **OPTION C**

4. **Mức độ an toàn quan trọng thế nào?**
   - Rất quan trọng (deadline gấp) → **OPTION A**
   - Trung bình → **OPTION B**
   - Có thể risk → **OPTION C**

---

## ⏭️ BƯỚC TIẾP THEO

Hãy làm 1 trong 2 việc sau:

**Cách 1: Để tôi phân tích FA25 tự động**
```powershell
# Copy FA25 vào workspace:
xcopy "C:\Users\THANH\Downloads\FA25_Gr7_Project_PRU213-main\FA25_Gr7_Project_PRU213-main" "c:\Users\THANH\Documents\GitHub\TowerDefense_GR06_PRU213\FA25_SOURCE" /E /I /H /Y
```

Sau đó nói với tôi: **"Tôi đã copy xong, phân tích đi"**

**Cách 2: Trả lời câu hỏi thủ công**

Trả lời các câu hỏi sau:

1. **FA25 Map 4 có những quái nào?** (tên quái)
2. **FA25 Map 5 có những quái nào?** (tên quái)
3. **Quái có skill đặc biệt không?** (ví dụ: shield, teleport, summon minions, v.v.)
4. **Map background có khác không?** (tên file png)
5. **Bạn muốn chọn OPTION nào?** (A, B, hay C)

---

## 📝 GHI CHÚ QUAN TRỌNG

### Logic Cần Giữ Nguyên:

**1. Fast-Forward Button:**
```csharp
// Trong UIController_Map4.cs, UIController_Map5.cs
private void SetGameSpeed(float timeScale) {
    GameManager.Instance.SetGameSpeed(timeScale);
}
```

**2. Resource Persistence:**
```csharp
// Trong GameManager
// DontDestroyOnLoad giữ GameManager qua scenes
// Resources không reset khi mission complete
```

**3. Mission Complete Flow:**
```csharp
// UIController hiện panel → Next Level button
// LevelManager.Instance.LoadLevel(nextLevel)
// GameManager KHÔNG reset resources
```

### Điểm Khác Biệt GR06 vs Thông Thường:

- Map 3 có **Hero Upgrade System** (Task 2 vừa làm xong)
- Map 5 có **Ability System** (IAbility interface)
- Fast-forward cần tương thích với tất cả skills/coroutines

---

**Tôi đang chờ bạn:**
1. Copy FA25 vào workspace, HOẶC
2. Trả lời các câu hỏi thủ công

Sau đó tôi sẽ đưa ra khuyến nghị cụ thể và bắt đầu merge! 🚀
