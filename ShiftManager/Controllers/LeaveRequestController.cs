using Microsoft.AspNetCore.Mvc;

namespace ShiftManager.Controllers
{
    public class LeaveRequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
