# DESIGN FILES - SOURCE FILES CHO MAP

## 📁 Thư mục này chứa

Đây là thư mục chứa các **source files gốc** được dùng để tạo map và assets trong dự án.

### Cấu trúc:

```
Design_Files/
├─ Map_Leve_1.aseprite        ← File Aseprite gốc của map (nếu có)
├─ Map_Leve_1_export.png      ← Export từ Aseprite
├─ Enemies/
│  ├─ Gobin_1.aseprite
│  ├─ slime.aseprite
│  └─ ...
└─ README.md                  ← File này
```

---

## 🎨 Workflow

### 1. Vẽ trong Aseprite

**File gốc:** `Map_Leve_1.aseprite`
- Canvas: 1920 x 1080 pixels
- Color Mode: RGBA
- Layers: Background, Terrain, Path, Decorations

### 2. Export PNG

**Aseprite Export:**
```
File → Export Sprite Sheet
- Format: PNG
- Output: Map_Leve_1_export.png
```

### 3. Copy vào Unity

**Command:**
```bash
copy Map_Leve_1_export.png ..\Assets\Asset_map_1\Asset\Map_Leve_1.png
```

---

## 🔄 Nếu cần chỉnh sửa

1. Mở file `.aseprite` trong thư mục này
2. Edit trong Aseprite
3. Export lại thành PNG
4. Copy/replace file trong Assets/

---

## ⚠️ Lưu ý

- **KHÔNG** xóa các file .aseprite - đây là source files gốc
- Luôn giữ backup
- Export với settings giống nhau để consistency

---

## 📝 Files đã tạo:

Ngày: 09/06/2026

✅ Map_Leve_1.aseprite → exported → Map_Leve_1.png (1.45 MB)  
✅ Gobin_1.aseprite → exported → Gobin_1.png (117 KB)  
✅ slime.aseprite → exported → slime.png (1 MB)  
✅ Slime_Boss.aseprite → exported → Slime_Boss.png (173 KB)  

Tool: Aseprite v1.3.7
OS: Windows
