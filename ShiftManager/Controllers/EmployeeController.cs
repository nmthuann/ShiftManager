using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftManager.Data;
using ShiftManager.Models;
using ShiftManager.ViewModels;

namespace ShiftManager.Controllers
{
    [Route("employees")]
    public class EmployeeController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        [Route("")]
        [HttpGet("")]
        public  IActionResult Index(int? branchId)
        {
            var branches =  _context.Branches.ToList();
            var employees = _context.Employees.Include(e => e.Branch).AsQueryable();

            if (branchId.HasValue)
            {
                employees = employees.Where(e => e.BranchId == branchId.Value);
            }

            var viewModel = new EmployeeViewModel
            {
                BranchId = branchId,
                Employees =  employees.ToList(),
                Branches = branches
            };

            return View( viewModel);
        }
        
        [HttpPost("")]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }
    }
}
