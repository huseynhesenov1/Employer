using Employe.DAL;
using Employe.Models;
using Employe.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Employe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Order> orders =  _context.Orders.ToList();
            return View(orders);
        }
        [HttpGet]
        public IActionResult Create()
        {
  
            ViewBag.Services = new SelectList(_context.Services, "Id", "Title");
            ViewBag.Masters = new SelectList(_context.Masters.Where(m => m.IsActive == true), "Id", "Name");

            return View();

        }


        [HttpPost]
        public IActionResult Create(OrderVM ordervm)
        {
            if (ModelState.IsValid)
            {
                Order order = new Order()
                {
                    ClientName = ordervm.ClientName,
                    ClientSurname = ordervm.ClientSurname,
                    ClientPhoneNumber = ordervm.ClientPhoneNumber,
                    ClientEmail = ordervm.ClientEmail,
                    ServiceId = ordervm.ServiceId,
                    MasterId = ordervm.MasterId,
                    Problem = ordervm.Problem,
                    IsActive = ordervm.IsActive,
                    CreatedAt = DateTime.Now,

                };

                _context.Orders.Add(order);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Services = new SelectList(_context.Services, "Id", "Title");
            ViewBag.Masters = new SelectList(_context.Masters.Where(m => m.IsActive == true), "Id", "Name");

            return View(ordervm);
        }

    }
}
