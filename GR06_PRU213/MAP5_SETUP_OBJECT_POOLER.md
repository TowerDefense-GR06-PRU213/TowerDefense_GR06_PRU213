# 🔧 FIX: Map 5 Spawner NullReferenceException

## ❌ LỖI MỚI

```
NullReferenceException: Object reference not set to an instance of an object
Spawner_Map5.SpawnEnemy (EnemyGroup_Map5 group) (at Assets/Asset_map_5/Scripts/Spawner_Map5.cs:242)
```

---

## 🔍 NGUYÊN NHÂN

**ObjectPooler chưa được gán trong Inspector!**

Spawner_Map5 cần 5 ObjectPooler cho 5 loại quái:
1. BongmaPool
2. BongmaacdocPool
3. XuongPool
4. PhuthuybongtoiPool
5. ChuatebongtoiPool

Nhưng trong Scene Map 5, các field này đang **TRỐNG** (null)!

---

## ✅ ĐÃ SỬA CODE

### Thêm Null Checks vào SpawnEnemy()

```csharp
private void SpawnEnemy(EnemyGroup_Map5 group)
{
    // ... path check ...
    
    if (_poolDictionary.TryGetValue(group.enemyType, out var pool))
    {
        // ✅ Kiểm tra pool có null không
        if (pool == null)
        {
            Debug.LogError($"ObjectPooler for {group.enemyType} is NULL! Check Inspector.");
            return;
        }

        GameObject spawnedObject = pool.GetPooledObject();

        // ✅ Kiểm tra GetPooledObject() trả về null
        if (spawnedObject == null)
        {
            Debug.LogError($"GetPooledObject() returned NULL for {group.enemyType}!");
            return;
        }

        // ✅ Kiểm tra enemy component
        Bongma enemy = spawnedObject.GetComponent<Bongma>();
        if (enemy == null)
        {
            Debug.LogError($"No Bongma component on {group.enemyType}!");
            return;
        }

        // ... rest of code ...
    }
}
```

Giờ thay vì crash, game sẽ hiện **error message rõ ràng** trong Console!

---

## 🎮 CÁCH FIX TRONG UNITY

### BƯỚC 1: Mở Scene Game_Map5

1. Mở Unity
2. Project panel → `Assets/Scene/Game_Map5.unity`
3. Double-click để mở

### BƯỚC 2: Tìm Spawner GameObject

1. Trong **Hierarchy**, tìm GameObject tên **"Spawner"** hoặc **"EnemySpawner"**
2. Click vào để chọn

### BƯỚC 3: Kiểm tra Inspector

Trong **Inspector**, tìm component **Spawner_Map5**:

```
Spawner_Map5 (Script)
├─ Waves (array)
├─ Bongma Pool          ← ❌ None (Object Pooler) - ĐANG TRỐNG!
├─ Bongmaacdoc Pool     ← ❌ None (Object Pooler) - ĐANG TRỐNG!
├─ Xuong Pool           ← ❌ None (Object Pooler) - ĐANG TRỐNG!
├─ Phuthuybongtoi Pool  ← ❌ None (Object Pooler) - ĐANG TRỐNG!
└─ Chuatebongtoi Pool   ← ❌ None (Object Pooler) - ĐANG TRỐNG!
```

### BƯỚC 4: Tạo Object Poolers

**CÁCH 1: Tạo thủ công (Khuyến nghị)**

Cho mỗi loại quái, tạo 1 GameObject:

1. **Right-click trong Hierarchy** → Create Empty
2. Đổi tên: `BongmaPool`
3. **Add Component** → Tìm `ObjectPooler`
4. Trong Inspector của ObjectPooler:
   ```
   - Pool Tag: "Bongma"
   - Prefab: Kéo prefab Bongma vào (từ Assets/Asset_map_5/Prefabs/)
   - Pool Size: 20
   ```

Lặp lại cho 4 quái còn lại:
- `BongmaacdocPool` → prefab `Bongmaacdoc`, size 15
- `XuongPool` → prefab `Xuong`, size 25
- `PhuthuybongtoiPool` → prefab `Phuthuybongtoi`, size 10
- `ChuatebongtoiPool` → prefab `Chuatebongtoi`, size 5

**CÁCH 2: Copy từ Map khác**

Nếu Map 1/2/3/4 đã có ObjectPooler setup:
1. Mở Scene Map 1
2. Copy các ObjectPooler GameObjects
3. Mở Scene Map 5
4. Paste vào Hierarchy
5. Đổi tên và prefabs cho phù hợp

### BƯỚC 5: Gán Poolers vào Spawner

1. Chọn **Spawner** GameObject
2. Trong **Inspector** → **Spawner_Map5** component:
   - **Bongma Pool**: Kéo GameObject `BongmaPool` vào
   - **Bongmaacdoc Pool**: Kéo GameObject `BongmaacdocPool` vào
   - **Xuong Pool**: Kéo GameObject `XuongPool` vào
   - **Phuthuybongtoi Pool**: Kéo GameObject `PhuthuybongtoiPool` vào
   - **Chuatebongtoi Pool**: Kéo GameObject `ChuatebongtoiPool` vào

### BƯỚC 6: Test

1. **Press Play**
2. Kiểm tra Console:
   - ✅ Không còn NullReferenceException
   - ✅ Quái spawn bình thường
   - ❌ Nếu vẫn lỗi → Xem debug message để biết vấn đề

---

## 🐛 DEBUG MESSAGES

Sau khi fix, nếu vẫn có vấn đề, Console sẽ hiện:

### Lỗi 1: Pool is NULL
```
ObjectPooler for Bongma is NULL! Check Inspector assignments in Spawner_Map5.
```
**Fix:** Gán ObjectPooler vào Spawner (Bước 5)

### Lỗi 2: GetPooledObject() returned NULL
```
GetPooledObject() returned NULL for Xuong! Pool might be empty or not initialized.
```
**Fix:** 
- Kiểm tra ObjectPooler có **Prefab** được gán chưa
- Tăng **Pool Size** lên (ví dụ: 20 → 30)

### Lỗi 3: No Bongma component
```
Spawned object for Phuthuybongtoi does not have Bongma component!
```
**Fix:**
- **SAI PREFAB!** Bạn gán nhầm prefab
- Map 5 tất cả enemies phải có script `Bongma.cs`
- Kiểm tra prefab trong `Assets/Asset_map_5/Prefabs/`

### Lỗi 4: Path not found
```
Path with name 'Path1' not found in the scene! Check path name in WaveData.
```
**Fix:**
- Kiểm tra Scene có GameObject tên "Path1" không
- Kiểm tra WaveData ScriptableObject có pathName đúng không

---

## 📋 CHECKLIST HOÀN CHỈNH

### Scene Setup:
- [ ] Scene Game_Map5 có GameObject "Spawner"
- [ ] Spawner có component Spawner_Map5
- [ ] Scene có 5 ObjectPooler GameObjects:
  - [ ] BongmaPool
  - [ ] BongmaacdocPool
  - [ ] XuongPool
  - [ ] PhuthuybongtoiPool
  - [ ] ChuatebongtoiPool

### ObjectPooler Setup (cho mỗi pool):
- [ ] Component ObjectPooler được add
- [ ] Pool Tag được điền (ví dụ: "Bongma")
- [ ] Prefab được gán (từ Assets/Asset_map_5/Prefabs/)
- [ ] Pool Size >= 10

### Spawner_Map5 Setup:
- [ ] Bongma Pool field → gán BongmaPool GameObject
- [ ] Bongmaacdoc Pool field → gán BongmaacdocPool GameObject
- [ ] Xuong Pool field → gán XuongPool GameObject
- [ ] Phuthuybongtoi Pool field → gán PhuthuybongtoiPool GameObject
- [ ] Chuatebongtoi Pool field → gán ChuatebongtoiPool GameObject

### Path Setup:
- [ ] Scene có GameObject "Path" hoặc tương tự
- [ ] Path có component Path (script)
- [ ] WaveData ScriptableObjects có pathName đúng

### Test:
- [ ] Press Play → Không có NullReferenceException
- [ ] Quái spawn được
- [ ] Quái di chuyển theo path
- [ ] Fast-forward button hoạt động

---

## 🎯 POOL SIZE KHUYẾN NGHỊ

| Enemy Type | Pool Size | Lý do |
|------------|-----------|-------|
| Bongma | 25 | Quái thường, xuất hiện nhiều |
| Xuong | 30 | Quái nhanh, spawn nhiều |
| Bongmaacdoc | 15 | Quái trung bình |
| Phuthuybongtoi | 12 | Quái mạnh, ít hơn |
| Chuatebongtoi | 5 | Boss, spawn ít |

**Lưu ý:** Nếu Map 5 có Endless Mode, tăng size lên 1.5x!

---

## ✅ KẾT QUẢ SAU KHI FIX

1. ✅ Không còn crash
2. ✅ Console hiện error messages rõ ràng (nếu còn vấn đề)
3. ✅ Dễ debug (biết chính xác pool nào bị thiếu)
4. ✅ Game chạy mượt với Object Pooling

---

## 🚀 HƯỚNG DẪN NHANH (TL;DR)

```
1. Mở Scene Game_Map5
2. Tạo 5 Empty GameObjects (1 cho mỗi loại quái)
3. Add Component → ObjectPooler vào mỗi GameObject
4. Gán Prefab + Pool Size cho mỗi ObjectPooler
5. Chọn Spawner → Gán 5 ObjectPooler vào các field
6. Press Play → Test!
```

---

Sau khi làm xong, Map 5 sẽ spawn quái bình thường! 🎉
