using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShiftManager.Models
{
    public class LeaveRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public required DateTime DayOff { get; set; }
        public required string Reason { get; set; }
        public required string OffType { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
