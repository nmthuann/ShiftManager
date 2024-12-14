using ShiftManager.Models;

namespace ShiftManager.ViewModels
{
    public class ShiftViewModel
    {
        public string CitizenId { get; set; }
        public string Name { get; set; }
        public string EngName { get; set; }
        public Dictionary<DateTime, string?> ShiftsByDay { get; set; } = new();
    }
}

