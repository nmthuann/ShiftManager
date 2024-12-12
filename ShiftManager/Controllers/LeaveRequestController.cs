﻿using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
                    DayOff = model.DayOff.ToUniversalTime(), // UTC
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
                ViewBag.ErrorMessage = ex.Message;
                Console.WriteLine(ex);
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
    }
}
