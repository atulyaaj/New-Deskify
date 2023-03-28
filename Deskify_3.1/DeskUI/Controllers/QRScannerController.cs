using Microsoft.AspNetCore.Mvc;

namespace DeskUI.Controllers
{
    public class QRScannerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
