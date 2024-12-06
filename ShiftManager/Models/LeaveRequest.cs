using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShiftManager.Commons.Enums;

namespace ShiftManager.Models
{
    public class LeaveRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required DateTime DayOff { get; set; }
        public required string Reason { get; set; }
        public required OffTypeEnum OffType { get; set; }

        public Guid EmployeeId { get; set; }
        public required Employee Employee { get; set; }
    }
}
