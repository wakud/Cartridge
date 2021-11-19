using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Cartridge.Models;
using Cartridge.Data;
using Cartridge.ViewModel;

namespace Cartridge.Controllers
{
    public class AccountController : Controller
    {
        private readonly MainContext db;

        public AccountController(MainContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users
                    .FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == Utils.EncryptDecrypt.Encrypt(model.Password));

                if (user != null)
                {
                    await Authenticate(user); // авторизація
                    return RedirectToAction("Index", "Home");   //перекидаємо користувача на основну сторінку
                }
                else
                {
                    ModelState.AddModelError("", "Не вірний логін і(або) пароль");
                    ViewBag.Error = "BedLogin";
                }
            }
            return View(model);
        }

        private async Task Authenticate(User user)
        {
            // створюємо один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim("AdminStatus", user.IsAdmin)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        //  вихід з програми
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        // GET: Admin/Create
        [Authorize(Policy = "OnlyForAdministrator")]
        public IActionResult Index()
        {
            return View();
        }

    }
}
