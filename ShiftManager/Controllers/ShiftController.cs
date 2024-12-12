using Microsoft.AspNetCore.Mvc;

namespace ShiftManager.Controllers
{
    public class ShiftController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
