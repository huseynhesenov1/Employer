using Microsoft.AspNetCore.Mvc;

namespace Employe.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
