# ShiftManager

# 🍔 McDonald's Internal Shift Scheduler

## 📝 Description

Ứng dụng nội bộ sắp xếp ca làm việc tại McDonald's hỗ trợ quản lý nhân sự và tổ chức lịch làm việc một cách hiệu quả. Ứng dụng được thiết kế nhằm giảm thiểu thời gian và công sức trong việc lập lịch làm việc, đồng thời cung cấp báo cáo chi tiết để hỗ trợ ra quyết định.

---

## 💻 Technical Approach

- **🖥️ Ngôn ngữ lập trình**: C#
- **🌐 Framework**: ASP.NET Core MVC
- **📦 ORM**: Entity Framework 9
- **🗄️ Cơ sở dữ liệu**: PostgreSQL

Ứng dụng được phát triển với kiến trúc MVC, đảm bảo tính mở rộng và bảo trì dễ dàng. Entity Framework 9 được sử dụng để làm việc với cơ sở dữ liệu, giúp đơn giản hóa các thao tác CRUD và truy vấn dữ liệu.

---

## 🚀 Features

### 1. **Quản lý nhân viên (CRUD Employee)**

- ➕ Thêm, ✏️ sửa, 🗑️ xóa và 📄 xem thông tin chi tiết của nhân viên.
- 🔑 Phân quyền theo chức vụ (nhân viên, quản lý, quản trị viên).

### 2. **Sắp xếp ca làm việc**

- 📋 Phân ca cho từng nhân viên dựa trên chức vụ, khung giờ làm việc và nhu cầu vận hành của cửa hàng.
- ⚠️ Cảnh báo xung đột lịch làm việc.

### 3. **Tạo lập báo cáo**

- 📊 Báo cáo chi tiết về lịch làm việc hàng tuần, hàng tháng.
- 📈 Thống kê số giờ làm việc của từng nhân viên và bộ phận.

### 4. **Tự động tạo lịch làm việc (Auto-Scheduler)**

- 🤖 Sử dụng thuật toán để tự động phân bổ ca làm việc dựa trên năng suất, ưu tiên cá nhân và yêu cầu của cửa hàng.

---

## 🛠️ Getting Started

### 📋 Prerequisites

- **.NET SDK**: .NET Core 8.0
- **Database**: PostgreSQL (phiên bản 13 trở lên)
- **IDE**: Visual Studio hoặc Visual Studio Code

### ⚙️ Installation

1. Clone repository:
   ```bash
   git clone https://github.com/nmthuann/ShiftManager.git
   cd ShiftManager
   ```

### 1️⃣ Cài đặt các package phụ thuộc

Chạy lệnh sau để tải và cài đặt tất cả các package được sử dụng trong dự án:

```bash
dotnet restore
```

### 2️⃣ Cấu hình chuỗi kết nối cơ sở dữ liệu

Mở file `appsettings.json` trong thư mục

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=YourDatabaseName;Username=YourUsername;Password=YourPassword"
}
```

### 3️⃣ Cấu hình chuỗi kết nối cơ sở dữ liệu

```bash
dotnet ef database update
```

### 4️⃣ Chạy ứng dụng

```bash
dotnet run
```

## 💡 Lưu ý

- Đảm bảo **PostgreSQL** đã được cài đặt và một cơ sở dữ liệu đã được tạo trước khi chạy lệnh migrations.
- Kiểm tra phiên bản **.NET Core** và **Entity Framework** để đảm bảo tương thích với dự án.
