using Microsoft.AspNetCore.Mvc;

namespace Employe.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
