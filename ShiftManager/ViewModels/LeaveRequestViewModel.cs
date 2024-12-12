using ShiftManager.Models;

namespace ShiftManager.ViewModels
{
    public class LeaveRequestViewModel
    {
        public  Employee Employee { get; set; }
        public  List<LeaveRequest> LeaveRequests { get; set; }
        public Guid EmployeeId;
    }
}
