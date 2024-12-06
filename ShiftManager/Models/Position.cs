using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShiftManager.Models
{
    public class Position
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string PositionName { get; set; } = string.Empty;
        public required int Priority { get; set; }
        public required decimal SalaryMultiplier { get; set; } // 0.0, 1.5
        public ICollection<Employee> Employees { get; set; } = [];
    }
}

