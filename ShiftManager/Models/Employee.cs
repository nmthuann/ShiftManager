using Microsoft.EntityFrameworkCore;
using System;

namespace ShiftManager.Models
{
    [Index(nameof(CitizenId), IsUnique = true)]
    public class Employee
    {
        public Guid Id { get; set; }
        public required string CitizenId { get; set; }
        public required string Name { get; set; }
        public required string EnglishName { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public required bool IsActive { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly Birthday { get; set; }

        public int BranchId { get; set; }
        public required Branch Branch  { get; set; }

        public int PositionId { get; set; }
        public required Position Position { get; set; }

        public ICollection<LeaveRequest>? LeaveRequests { get; set; } = [];

        public ICollection<ShiftAssignment>? ShiftAssignments { get; set; } = [];
    }
}
