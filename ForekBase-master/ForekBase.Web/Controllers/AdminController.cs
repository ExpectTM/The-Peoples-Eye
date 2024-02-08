using Microsoft.AspNetCore.Mvc;

namespace ForekBase.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult News()
        {
            return View();
        }
    }
}
