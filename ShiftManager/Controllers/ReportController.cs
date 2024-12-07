using Microsoft.AspNetCore.Mvc;

namespace ShiftManager.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
