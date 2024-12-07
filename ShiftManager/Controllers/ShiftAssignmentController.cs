using Microsoft.AspNetCore.Mvc;

namespace ShiftManager.Controllers
{
    public class ShiftAssignmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
