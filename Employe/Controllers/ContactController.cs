using Microsoft.AspNetCore.Mvc;

namespace Employe.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
