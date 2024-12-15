using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShiftManager.Data;
using ShiftManager.Models;
using ShiftManager.ViewModels;


namespace ShiftManager.Controllers
{
    [Route("shifts")]
    public class ShiftController(AppDbContext context) : Controller
    {
        const int _numberOfWeeks = 3;
        private readonly AppDbContext _context = context;

        [HttpGet("")]
        public IActionResult Index(int ?optionIndex, int ?BranchId)
        {
            var weekRanges = GenerateWeekRanges(DateTime.Now, _numberOfWeeks);
            ViewBag.WeekOptions = weekRanges;
            ViewBag.BranchId = new SelectList(_context.Branches, "Id", "BranchName");
            
            if(optionIndex != null && BranchId != null)
            {
                Console.WriteLine(optionIndex + BranchId);
                var weekRangesRender = GenerateWeekRanges(DateTime.Now, _numberOfWeeks);
                if (optionIndex < 1 || optionIndex > weekRangesRender.Count)
                {
                    return BadRequest("Invalid option index.");
                }

                var selectedWeekRange = weekRangesRender[(int)optionIndex - 1];
                var weekParts = selectedWeekRange.Split(" - ");
                var startDate = DateOnly.ParseExact(weekParts[0], "dd/MM/yyyy", null);
                var endDate = DateOnly.ParseExact(weekParts[1], "dd/MM/yyyy", null);

                

  

                var employees = 
                    _context.Employees
                    .Where(e => e.IsActive)
                    .Include(e => e.Position)
                    .OrderByDescending(e => e.Position.Priority)
                    .ThenBy(e => e.Name)
                    .ToList();

                var shiftAssignment = _context.ShiftAssignments
                    .Include(sa => sa.Shift)
                    .Where(sa => sa.DateAssigned >= startDate &&  sa.DateAssigned <= endDate)
                    .ToList();

                var leaveRequests = _context.LeaveRequests
                    .Where(lr => lr.DayOff >= startDate && lr.DayOff <= endDate)
                    .ToList();

                var shiftAssignments = _context.ShiftAssignments
                  .Include(sa => sa.Shift)
                  .Where(sa => sa.DateAssigned >= startDate && sa.DateAssigned <= endDate)
                  .ToList();

                var shiftViewModels = employees.Select(e => new ShiftViewModel
                {
                    CitizenId = e.CitizenId,
                    Name = e.Name,
                    EngName = e.EnglishName,
                    ShiftsByDay = Enumerable.Range(0, 7)
                .Select(offset => startDate.AddDays(offset))
                .ToDictionary(
                    date => date,
                    date =>
                    {
                        // Kiểm tra ngày nghỉ trước
                        var leave = leaveRequests
                            .FirstOrDefault(lr => lr.EmployeeId == e.Id && lr.DayOff.Date == date.Date);

                        if (leave != null)
                        {
                            return leave.OffType; // "UL", "AL", hoặc "OFF"
                        }

                        // Nếu không có ngày nghỉ, hiển thị ca làm việc
                        return shiftAssignments
                            .FirstOrDefault(sa => sa.EmployeeId == e.Id && sa.DateAssigned.Date == date.Date)?.Shift.ShiftName ?? "OFF";
                    }
                )
                }).ToList();


                ViewBag.TableHeader = GenerateTableHeader(startDate, endDate);
                ViewBag.SelectedWeek = selectedWeekRange;
                return View(shiftViewModels);
            }
            return View();
        }

        private List<string> GenerateTableHeader(DateTime startDate, DateTime endDate)
        {
            var headers = new List<string> { "ID No", "Full Name", "Eng Name" };
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                headers.Add($"{date:ddd dd/MM}");
            }
            return headers;
        }

        private List<string> GenerateWeekRanges(DateTime startDate, int numberOfWeeks)
        {
            var weekRanges = new List<string>();

            int daysUntilMonday = (DayOfWeek.Monday - startDate.DayOfWeek + 7) % 7;
            startDate = startDate.AddDays(daysUntilMonday);
            
            for(int i = 0; i<numberOfWeeks; i++)
            {
                var startOfWeek = startDate.AddDays(i * 7);
                var endOfWeek= startOfWeek.AddDays(6);

                weekRanges.Add($"{startOfWeek:dd/MM/yyyy} - {endOfWeek:dd/MM/yyyy}");
            }

            return weekRanges;
        }
    }
}
