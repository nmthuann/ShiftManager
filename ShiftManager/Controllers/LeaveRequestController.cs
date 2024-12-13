using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShiftManager.Commons.Enums;
using ShiftManager.Data;
using ShiftManager.Models;
using ShiftManager.ViewModels;

namespace ShiftManager.Controllers
{
    [Route("leave-requests")]
    public class LeaveRequestController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("search")]
        public IActionResult Index(string citizenId)
        {
            if (string.IsNullOrEmpty(citizenId))
            {
                ViewBag.Message = "Please enter a search term.";
                return View();
            }
            try
            {
                var employee = _context.Employees.FirstOrDefault(e => e.CitizenId.Equals(citizenId));
                if (employee == null)
                {
                    ViewBag.Message = "No employee found with the given Citizen ID.";
                    return View();
                }

                var leaveRequests = _context.LeaveRequests
                    .Where(lr => lr.EmployeeId == employee.Id)
                    .ToList();

                var viewModel = new LeaveRequestViewModel
                {
                    Employee = employee,
                    LeaveRequests = leaveRequests
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                ViewBag.Message = "No employee found with the given Citizen ID.";
                return View();
            }
        }


        [HttpGet("create")]
        public IActionResult Create(Guid? employeeId)
        {
            if (employeeId == null)
            {
                return RedirectToAction("Index");
            }

            var employee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);
            if (employee == null)
            {
                ViewBag.Message = "Employee not found.";
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = employee.Id;
            ViewBag.OffTypeList = Enum.GetValues(typeof(OffTypeEnum))
                                      .Cast<OffTypeEnum>()
                                      .Select(e => new SelectListItem
                                      {
                                          Value = e.ToString(),
                                          Text = e.ToString()
                                      }).ToList();
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(LeaveRequest model)
        {
            var employee = _context.Employees.Find(model.EmployeeId);
            if (employee == null) {
                return BadRequest();
            }

            try
            {
                var leaveRequest = new LeaveRequest
                {
                    DayOff = model.DayOff, // UTC
                    Reason = model.Reason,
                    OffType = model.OffType,
                    EmployeeId = employee.Id,
                    Employee = employee
                };
                     
                _context.LeaveRequests.Add(leaveRequest);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.EmployeeId = employee.Id;
                ViewBag.OffTypeList = Enum.GetValues(typeof(OffTypeEnum))
                                          .Cast<OffTypeEnum>()
                                          .Select(e => new SelectListItem
                                          {
                                              Value = e.ToString(),
                                              Text = e.ToString()
                                          }).ToList();
                return View(model);
            }
        }


        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var leave = _context.LeaveRequests.Find(id);
            if (leave == null) {
                return BadRequest(); 
            }  
            ViewBag.EmployeeId = leave.EmployeeId;
            ViewBag.OffTypeList = Enum.GetValues(typeof(OffTypeEnum))
                                      .Cast<OffTypeEnum>()
                                      .Select(e => new SelectListItem
                                      {
                                          Value = e.ToString(),
                                          Text = e.ToString()
                                      }).ToList();
            return View(leave);
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id, LeaveRequest model)
        {
            var findLeaveReq = this._context.LeaveRequests.Find(id);

            if (findLeaveReq == null)
            {
                return BadRequest();
            }

            findLeaveReq.DayOff = model.DayOff;
            findLeaveReq.OffType = model.OffType;
            findLeaveReq.Reason = model.Reason;

            try
            {
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.EmployeeId = findLeaveReq.EmployeeId;
                ViewBag.OffTypeList = Enum.GetValues(typeof(OffTypeEnum))
                                          .Cast<OffTypeEnum>()
                                          .Select(e => new SelectListItem
                                          {
                                              Value = e.ToString(),
                                              Text = e.ToString()
                                          }).ToList();
                return View(model);
            }
        }
    }
}
