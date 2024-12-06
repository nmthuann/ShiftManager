using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShiftManager.Models
{
    public class Branch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string BranchName { get; set; }
        public required string Address { get; set; }
        public required string Contact { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
