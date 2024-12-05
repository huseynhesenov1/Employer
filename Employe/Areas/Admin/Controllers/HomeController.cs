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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _context = appDbContext;
            _webHostEnvironment = webHostEnvironment;
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
            if (!ModelState.IsValid)
            {
                ViewBag.Services = new SelectList(_context.Services, "Id", "Title");
                ViewBag.Masters = new SelectList(_context.Masters.Where(m => m.IsActive == true), "Id", "Name");
                return View(ordervm);
            }
            if (ordervm.Img is null)
            {
                return View(ordervm);
            }




            string fileName = Path.GetFileNameWithoutExtension(ordervm.Img.FileName);
            if (ordervm.Img.Length>2*1024*1024)
            {
                ModelState.AddModelError("Img", "Shekilin ölçüsü 2 Mb dan çox ola bilməz");
            }

            string[] AllowFormat = [".png", ".jpg", ".jpeg"];
            string extension = Path.GetExtension(ordervm.Img.FileName);
            bool isAlllowed = false;
            foreach(var item in AllowFormat)
            {
                if (item == extension)
                {
                    isAlllowed = true;
                    break;
                    
                }
            }
            if (!isAlllowed)
            {
                ModelState.AddModelError("Img", "Bu formata icaze yoxdur");
                return View(ordervm);
            }



            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "ImageUpload");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            if (Path.Exists(Path.Combine(uploadPath, fileName+extension)))
            {
                fileName = fileName + Guid.NewGuid().ToString();
            }

            fileName = fileName + extension;
            uploadPath = Path.Combine(uploadPath, fileName);    

            using FileStream fileStream = new FileStream(uploadPath,FileMode.Create);
            ordervm.Img.CopyTo(fileStream);



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
                    CreatedAt = DateTime.Now,
                    ImgPath = fileName

                };

                _context.Orders.Add(order);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            

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
