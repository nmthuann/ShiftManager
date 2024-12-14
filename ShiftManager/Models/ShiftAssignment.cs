using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ShiftManager.Models
{
    [Index(nameof(DateAssigned), nameof(ShiftId), IsUnique = true)]
    public class ShiftAssignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime DateAssigned { get; set; }


        public int ShiftId { get; set; }
        public required Shift Shift { get; set; }

        public Guid EmployeeId { get; set; }
        public required Employee Employee { get; set; }
    }
}
