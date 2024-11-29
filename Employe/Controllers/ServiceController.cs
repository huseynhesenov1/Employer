using Microsoft.AspNetCore.Mvc;

namespace Employe.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
