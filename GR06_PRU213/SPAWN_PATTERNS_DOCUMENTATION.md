# QUY LUẬT SPAWN QUÁI - MAP 1, MAP 2, MAP 3

## TỔNG QUAN HỆ THỐNG SPAWN

Mỗi map có cơ chế spawn riêng:
- **Map 1**: Wave ScriptableObject + Multiple Paths
- **Map 2**: Wave Array trong Prefab + 2 Paths (A/B)
- **Map 3**: Wave ScriptableObject + Auto-balance Paths

---

## MAP 1 - SPAWN PATTERNS

### Cấu trúc
- **File config**: `Assets/Asset_map_1/ScriptableObjects/Wave__Map1/Wave1-5.asset`
- **Script logic**: `Spawner_Map1.cs`
- **Số wave để thắng**: 5 waves (mặc định)
- **Thời gian giữa các wave**: Có thể tùy chỉnh trong code

### Loại quái (EnemyType_Map1)
0. **Slime** - Quái cơ bản
1. **Gobin** - Có skill né tránh
2. **Slime_phan_no** - Tăng tốc khi máu thấp
3. **Gobin_cung_thu** - Yếu với đòn tầm xa
4. **Slime_Boss** - Boss, spawn minions

### Chi tiết từng wave

#### **Wave 1**
- **Delay**: 1.5 giây
- **Nhóm 1**: 5 Slimes trên Path1 (spawn mỗi 2s)
- **Nhóm 2**: 3 Gobins trên Path2 (spawn mỗi 2s)
- **Nhóm 3**: 2 Slimes trên Path1 (spawn mỗi 1s)
- **Tổng**: 10 quái

#### **Wave 2**
- **Delay**: 2 giây
- **Nhóm 1**: 3 Slime Phản Nộ trên Path1 (spawn mỗi 1.5s)
- **Nhóm 2**: 4 Gobins trên Path2 (spawn mỗi 2s)
- **Nhóm 3**: 3 Slimes trên Path1 (spawn mỗi 1s)
- **Tổng**: 10 quái

#### **Wave 3**
- **Delay**: 3 giây
- **Nhóm 1**: 3 Gobin Cung Thủ trên Path2 (spawn mỗi 2s)
- **Nhóm 2**: 4 Slime Phản Nộ trên Path1 (spawn mỗi 2s)
- **Nhóm 3**: 3 Gobins trên Path2 (spawn mỗi 1.5s)
- **Tổng**: 10 quái

#### **Wave 4**
- **Delay**: 4.5 giây
- **Nhóm 1**: 4 Gobin Cung Thủ trên Path2 (spawn mỗi 1.5s)
- **Nhóm 2**: 3 Slime Phản Nộ trên Path1 (spawn mỗi 2s)
- **Tổng**: 7 quái

#### **Wave 5** (BOSS WAVE)
- **Delay**: 5 giây
- **Nhóm 1**: 4 Gobin Cung Thủ trên Path1 (spawn mỗi 1s)
- **Nhóm 2**: **1 Boss Slime** trên Path2 (spawn sau 3s)
- **Nhóm 3**: 4 Slime Phản Nộ trên Path2 (spawn mỗi 2.5s)
- **Nhóm 4**: 5 Slimes trên Path1 (spawn mỗi 1.5s)
- **Tổng**: 14 quái + Boss (Boss sẽ spawn thêm minions)

### Đặc điểm spawn Map 1
- Mỗi **nhóm quái** trong wave có thể đi **path riêng** (Path1 hoặc Path2)
- Wave có `startDelay` trước khi bắt đầu spawn
- Mỗi nhóm spawn tuần tự (nhóm 1 xong → nhóm 2 → nhóm 3...)
- **Logic thắng**: Phải spawn đủ 5 waves VÀ tiêu diệt hết quái
- **Endless Mode**: Sau khi thắng, người chơi có thể chọn tiếp tục → lặp lại Wave 1-5 mãi mãi

---

## MAP 2 - SPAWN PATTERNS

### Cấu trúc
- **File config**: `Assets/Asset_map_2/Prefab/EnemyWaveSpawner.prefab` (cấu hình wave trong prefab)
- **Script logic**: `EnemyWaveSpawner.cs`
- **Số wave**: Được cấu hình trong mảng `waves[]` trong prefab
- **Thời gian giữa các wave**: `timeBetweenWaves = 5f`

### Loại quái Map 2
- **Orc** - Quái cơ bản
- **Zombie** - Quái cơ bản
- **Golem** - Tự buff tốc độ khi bị đánh
- **Tiny Golem** - Buff đồng đội (NGUY HIỂM NHẤT)
- **Valkyrie** - Có khiên giảm sát thương

### Cấu trúc wave Map 2

Mỗi wave có:
```csharp
- waveName: Tên wave
- enemyGroups[]: Danh sách nhóm quái
  * enemyPrefab: Prefab của quái
  * count: Số lượng
  * spawnDelay: Thời gian giữa mỗi con (giây)
  * path: PathType.A hoặc PathType.B
- reward: Tiền thưởng khi hoàn thành wave
```

### Đặc điểm spawn Map 2
- Có **2 điểm spawn**: `spawnPointA` và `spawnPointB`
- Mỗi nhóm quái được chỉ định đi path A hoặc path B
- Spawn tuần tự từng nhóm trong wave
- **Logic thắng**: Hoàn thành tất cả waves VÀ cổng (Gate) vẫn sống
- **Reward system**: Mỗi wave cho tiền thưởng sau khi hoàn thành
- Nếu cổng chết → Game Over ngay lập tức

**LƯU Ý**: Chi tiết cụ thể từng wave cần kiểm tra trong Unity Editor (mở prefab `EnemyWaveSpawner`)

---

## MAP 3 - SPAWN PATTERNS

### Cấu trúc
- **File config**: `Assets/Asset_map_3/ScriptableObjects/Wave/Wave1-5.asset`
- **Script logic**: `Spawner_Lv3.cs`
- **Số wave để thắng**: 5 waves (mặc định)
- **Thời gian giữa các wave**: `timeBetweenWaves = 5f`

### Loại quái (EnemyType_Lv3)
0. **Yeti** - Quái cơ bản
1. **YetiTanker** - Máu cao
2. **PhuThuyBang** - Có khiên tái sinh
3. **SnowMan** - Enrage khi máu thấp
4. **BossYeti** - Boss, triệu hồi Yeti minions

### Chi tiết từng wave

#### **Wave 1**
- **Delay**: 3 giây
- **Nhóm 1**: 10 Yetis (spawn mỗi 3s)
- **Reward**: 300 gold
- **Tổng**: 10 quái

#### **Wave 2**
- **Delay**: 0 giây
- **Nhóm 1**: 8 Yeti Tankers (spawn mỗi 3s)
- **Reward**: 336 gold
- **Tổng**: 8 quái

#### **Wave 3**
- **Delay**: 0 giây
- **Nhóm 1**: 8 Yetis (spawn mỗi 2s)
- **Nhóm 2**: 5 Yeti Tankers (spawn mỗi 3s, delay 10s)
- **Nhóm 3**: 6 Phù Thủy Băng (spawn mỗi 2.5s, delay 10s)
- **Nhóm 4**: 1 Boss Yeti (spawn sau 5s delay)
- **Reward**: 540 gold
- **Tổng**: 20 quái + 1 Boss

#### **Wave 4**
- **Delay**: 0 giây
- **Nhóm 1**: 12 Yetis (spawn mỗi 2s)
- **Nhóm 2**: 10 Phù Thủy Băng (spawn mỗi 3s, delay 10s)
- **Nhóm 3**: 5 Người Tuyết (spawn mỗi 3.5s, delay 10s)
- **Reward**: 384 gold
- **Tổng**: 27 quái

#### **Wave 5** (FINAL BOSS WAVE)
- **Delay**: 0 giây
- **Nhóm 1**: 6 Yeti Tankers (spawn mỗi 2s)
- **Nhóm 2**: 5 Phù Thủy Băng (spawn mỗi 1s, delay 10s)
- **Nhóm 3**: 4 Người Tuyết (spawn mỗi 10s, delay 10s)
- **Nhóm 4**: **2 Boss Yetis** (spawn mỗi 5s, delay 10s)
- **Nhóm 5**: 5 Yetis (spawn mỗi 15s, delay 5s)
- **Reward**: 770 gold
- **Tổng**: 22 quái + 2 Boss

### Đặc điểm spawn Map 3

1. **Auto-balance Path System**:
   - Map 3 có nhiều paths (Path1, Path2, Path3...)
   - Spawner tự động **luân phiên** spawn quái trên các path
   - Quái 1 → Path1, Quái 2 → Path2, Quái 3 → Path3, Quái 4 → Path1...
   - Đảm bảo **cân bằng áp lực** trên tất cả các path

2. **Enemy Groups với Delay**:
   - Mỗi nhóm có `delayBeforeGroup` (thời gian chờ trước khi spawn nhóm này)
   - Các nhóm spawn **tuần tự** (nhóm 1 → chờ delay → nhóm 2...)

3. **Reward System**:
   - Mỗi wave cho gold sau khi hoàn thành

4. **Logic thắng**:
   - Phải hoàn thành đủ 5 waves
   - **Không** cần giết hết quái của wave trước mới spawn wave sau (chỉ cần chờ delay)
   - Khi spawn đủ 5 waves → hiển thị "Mission Complete"
   - Người chơi có thể chọn **Endless Mode** để tiếp tục

5. **Endless Mode**:
   - Lặp lại Wave 1-5 mãi mãi
   - Wave spawn liên tục, không chờ giết hết quái

---

## SO SÁNH 3 MAP

| Đặc điểm | Map 1 | Map 2 | Map 3 |
|----------|-------|-------|-------|
| **Config** | ScriptableObject | Prefab Array | ScriptableObject |
| **Số Paths** | 2 (Path1, Path2) | 2 (A, B) | Nhiều (auto-balance) |
| **Path Selection** | Thủ công (mỗi nhóm) | Thủ công (mỗi nhóm) | Tự động luân phiên |
| **Wave Condition** | Spawn xong + giết hết | Spawn xong + giết hết | Chỉ cần spawn đủ wave |
| **Boss Wave** | Wave 5 | Tùy config | Wave 3 & 5 |
| **Reward** | Không | Có (mỗi wave) | Có (mỗi wave) |
| **Endless Mode** | Có | Không | Có |
| **Delay System** | `startDelay` (wave) | `spawnDelay` (nhóm) | `delayBeforeGroup` (nhóm) |

---

## CÁC KHÁI NIỆM QUAN TRỌNG

### 1. Wave vs Enemy Group
- **Wave**: Một đợt tấn công lớn (VD: Wave 1, Wave 2...)
- **Enemy Group**: Một nhóm quái nhỏ trong wave (VD: 5 Slimes, 3 Gobins...)

### 2. Spawn Interval
- Thời gian giữa mỗi con quái trong cùng nhóm
- VD: `spawnInterval = 2s` → spawn con 1 → chờ 2s → spawn con 2 → chờ 2s...

### 3. Wave Delay
- **Map 1**: `startDelay` - delay trước khi wave bắt đầu
- **Map 2**: `timeBetweenWaves` - delay giữa các wave
- **Map 3**: `delayBeforeGroup` - delay trước khi nhóm bắt đầu spawn

### 4. Path Assignment
- **Map 1 & 2**: Mỗi nhóm quái được chỉ định path cụ thể
- **Map 3**: Spawner tự động phân bổ quái đều trên tất cả paths

### 5. Win Condition
- **Map 1**: Spawn đủ 5 waves + tiêu diệt hết quái
- **Map 2**: Hoàn thành tất cả waves + Gate còn sống
- **Map 3**: Spawn đủ 5 waves (không cần giết hết)

---

## LƯU Ý KHI CHỈNH SỬA

### Để thay đổi độ khó:
1. **Tăng số lượng quái**: Sửa `count` trong wave config
2. **Tăng tốc độ spawn**: Giảm `spawnInterval` hoặc `delayBeforeGroup`
3. **Thêm quái mạnh**: Thay đổi `enemyType` thành loại quái khó hơn
4. **Thêm wave**: Tạo Wave6.asset, Wave7.asset... và thêm vào mảng `waves[]`

### Để test một mình:
- Mở scene map và nhấn Play trong Unity Editor
- Không cần LevelManager, các GameManager đã có giá trị mặc định (5 lives, 500 gold)

### Để kiểm tra cấu hình wave:
- **Map 1 & 3**: Mở file .asset trong Inspector
- **Map 2**: Mở prefab `EnemyWaveSpawner` và xem mảng `waves[]`
