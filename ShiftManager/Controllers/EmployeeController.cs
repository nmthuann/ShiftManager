using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

  
        [HttpGet("")]
        public  IActionResult Index(int branchId)
        {
            var branches =  _context.Branches.ToList();
            var positions = _context.Positions.ToList();
            var employees = _context.Employees.Include(e => e.Branch).AsQueryable();

            //if (branchId != null)
            //{
            //    employees = employees.Where(e => e.BranchId == branchId);
            //}

            var viewModel = new EmployeeViewModel
            {
                BranchId = branchId,
                Employees = employees.ToList(),
                Branches = branches,
                Positions = positions
            };

            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewBag.BranchId = new SelectList(_context.Branches, "Id", "BranchName");
            ViewBag.PositionId = new SelectList(_context.Positions, "Id", "PositionName");
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee model)
        {
            Console.WriteLine(
                $"" +
                $"CitizenId: {model.CitizenId}, " +
                $"Name: {model.Name}, " +
                $"BranchId: {model.BranchId}, PositionId: {model.PositionId}, EnglishName: {model.EnglishName}, Address: {model.Address}, Phone: {model.Phone}, StartDate:{model.StartDate},StartDate:{model.Birthday}");

            try
            {
                // Fetch the related Branch and Position entities explicitly
                var branch = _context.Branches.FirstOrDefault(b => b.Id == model.BranchId);
                var position = _context.Positions.FirstOrDefault(p => p.Id == model.PositionId);

                // If either the branch or position is invalid, return with an error
                if (branch == null || position == null)
                {
                    ModelState.AddModelError("", "Invalid Branch or Position.");
                    ViewBag.BranchId = new SelectList(_context.Branches, "Id", "BranchName", model.BranchId);
                    ViewBag.PositionId = new SelectList(_context.Positions, "Id", "PositionName", model.PositionId);
                    return View(model);
                }

                // Set the related entities to the model
                model.Branch = branch;
                model.Position = position;

                // Save the new employee
                _context.Employees.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Repopulate ViewBag for dropdowns
                ViewBag.BranchId = new SelectList(_context.Branches, "Id", "BranchName", model.BranchId);
                ViewBag.PositionId = new SelectList(_context.Positions, "Id", "PositionName", model.PositionId);

                return View(model);
            }
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(Guid id)
        {
            var employee = _context.Employees
                .Include(e => e.Branch)
                .Include(e => e.Position)
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            ViewBag.BranchId = new SelectList(_context.Branches, "Id", "BranchName", employee.BranchId);
            ViewBag.PositionId = new SelectList(_context.Positions, "Id", "PositionName", employee.PositionId);

            return View(employee);
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(Guid id, Employee model) {
            var findEmp = this._context.Employees.Find(id);
            if (findEmp == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                ViewBag.BranchId = new SelectList(_context.Branches, "Id", "BranchName", model.BranchId);
                ViewBag.PositionId = new SelectList(_context.Positions, "Id", "PositionName", model.PositionId);
                return View(model);
            }

            findEmp.CitizenId = model.CitizenId;
            findEmp.Name = model.Name;
            findEmp.EnglishName = model.EnglishName;
            findEmp.Phone = model.Phone;
            findEmp.Address = model.Address;
            findEmp.IsActive = model.IsActive;
            findEmp.StartDate = model.StartDate;
            findEmp.Birthday = model.Birthday;
            findEmp.BranchId = model.BranchId;
            findEmp.PositionId = model.PositionId;


            try
            {
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.BranchId = new SelectList(_context.Branches, "Id", "BranchName", model.BranchId);
                ViewBag.PositionId = new SelectList(_context.Positions, "Id", "PositionName", model.PositionId);
                return View(model);
            }
        }
    }

}
