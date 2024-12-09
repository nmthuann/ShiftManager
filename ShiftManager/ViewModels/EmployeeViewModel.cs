using ShiftManager.Models;

namespace ShiftManager.ViewModels
{
    public class EmployeeViewModel
    {
        public int BranchId { get; set; } 
        public DateTime Birthday { get; set; }
        public DateTime StartDate { get; set; }
        public int PositionId { get; set; }
        public required List<Employee> Employees { get; set; }
        public required List<Branch> Branches { get; set; }  
        public required List<Position> Positions { get; set; }
    }
}
