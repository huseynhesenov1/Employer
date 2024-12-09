using Employe.DAL;
using Employe.DTOs.UserDTOs;
using Employe.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Employe.Controllers;




public class AccountsController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public AccountsController(UserManager<AppUser> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(CreateUserDto createUserDto)
    {
        if (!ModelState.IsValid)
        {
            return View(createUserDto);
        }
        AppUser user = new AppUser();
        user.FirstName = createUserDto.FirstName;
        user.LastName = createUserDto.LastName;
        user.Email = createUserDto.Email;
        user.UserName = createUserDto.UserName;
        var result = await _userManager.CreateAsync(user, createUserDto.Password);

        if (!result.Succeeded)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(item.Code, item.Description);
            }
            return View(createUserDto);

        }

        return RedirectToAction("Index", "Home");
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]

    public IActionResult Login(CreateUserDto createUserDto)
    {
        return View();
    }



















    //[HttpPost]
    //public async Task<IActionResult> Register(CreateUserDto createUserDto)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return View(createUserDto);
    //    }
    //    AppUser user = new AppUser();
    //    user.FirstName = createUserDto.FirstName;
    //    user.LastName = createUserDto.LastName;
    //    user.Email = createUserDto.Email;
    //    user.UserName = createUserDto.UserName;
    //    var result = await _userManager.CreateAsync(user, createUserDto.Password);

    //    if (!result.Succeeded)
    //    {
    //        foreach (var item in result.Errors)
    //        {
    //            ModelState.AddModelError(item.Code, item.Description);
    //        }
    //        return View(createUserDto);
    //    }
    //    await _userManager.AddToRoleAsync(user, "User");

    //    //await _signInManager.SignInAsync(user, isPersistent: true);

    //    return RedirectToAction(nameof(Login), "Accounts");
    //}
}
