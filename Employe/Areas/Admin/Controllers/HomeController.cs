using Employe.DAL;
using Employe.Models;
using Employe.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            IEnumerable<Order> orders = _context.Orders.ToList();
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
        public IActionResult Update(int Id)
        {
            Order? updateOrder = _context.Orders.Find(Id);
            if (updateOrder == null)
            {
                return NotFound("Bele Sifarish yoxdur");
            }

            OrderVM orderVM = new OrderVM()
            {
                Id = updateOrder.Id, 
                ClientName = updateOrder.ClientName,
                ClientEmail = updateOrder.ClientEmail,
                ClientSurname = updateOrder.ClientSurname,
                ClientPhoneNumber = updateOrder.ClientPhoneNumber,
                ServiceId = updateOrder.ServiceId,
                MasterId = updateOrder.MasterId,
                Problem = updateOrder.Problem,
                IsActive = updateOrder.IsActive,
                CreatedAt = updateOrder.CreatedAt
            };

            ViewBag.Services = new SelectList(_context.Services, "Id", "Title");
            ViewBag.Masters = new SelectList(_context.Masters.Where(m => m.IsActive == true), "Id", "Name");

            return View(orderVM);
        }
        [HttpPost]

        public IActionResult Update(OrderVM orderVM)
        {
            ViewBag.Services = new SelectList(_context.Services, "Id", "Title");
            ViewBag.Masters = new SelectList(_context.Masters.Where(m => m.IsActive == true), "Id", "Name");

            if (orderVM.ServiceId <= 0 || orderVM.MasterId <= 0)
            {
                ModelState.AddModelError("", "Service or MAsters seçilməyib.");
                return View(orderVM);
            }

            if (!ModelState.IsValid)
            {
                return View(orderVM);
            }
            Order? updateOrder = _context.Orders.FirstOrDefault(x => x.Id == orderVM.Id);
            if (updateOrder == null)
            {
                return NotFound("Belə Sifariş yoxdur");
            }
            updateOrder.ClientName = orderVM.ClientName;
            updateOrder.ClientEmail = orderVM.ClientEmail;
            updateOrder.ClientSurname = orderVM.ClientSurname;
            updateOrder.ClientPhoneNumber = orderVM.ClientPhoneNumber;
            updateOrder.ServiceId = orderVM.ServiceId;
            updateOrder.MasterId = orderVM.MasterId;
            updateOrder.Problem = orderVM.Problem;
            updateOrder.IsActive = orderVM.IsActive;
            updateOrder.UpdatedAt = DateTime.Now;

            _context.Orders.Update(updateOrder);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult SOftDelete(int Id)
        {
            Order? order = _context.Orders.Find(Id);
            if (order == null)
            {
                return NotFound("Not Found Service");
            }

            order.IsActive = false;

            //_context.SliderItems.Remove(sliderItem);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index), "Home");
        }

    }
}
