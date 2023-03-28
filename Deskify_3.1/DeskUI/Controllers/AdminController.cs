using Microsoft.AspNetCore.Mvc;

namespace DeskUI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
