using ShiftManager.Models;

namespace ShiftManager.ViewModels
{
    public class EmployeeViewModel
    {
        public int? BranchId { get; set; }  // Lưu trữ ID của chi nhánh đã chọn
        public required List<Employee> Employees { get; set; }  // Danh sách nhân viên
        public required List<Branch> Branches { get; set; }  // Danh sách chi nhánh
    }
}
