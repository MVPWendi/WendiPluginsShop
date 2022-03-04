using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using UnturnedWebForum.Models;
using SteamTest.Models;
using System;

namespace UnturnedWebForum.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserContext db;
        public AccountController(UserContext context)
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
                User user = await db.Users.FirstOrDefaultAsync(u => u.UserName == model.UserName && u.Password == model.Password);
                
                if (user != null)
                {
                    user.Role = await db.Roles.FirstOrDefaultAsync(p => p.Id == user.RoleId);
                    
                    await Authenticate(user); // аутентификация
                   
                    return RedirectToAction("UserProfile", "Profile");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => (u.Email == model.Email || u.UserName == model.UserName));
                if (user == null)
                {
                    // добавляем пользователя в бд
                    var role = await db.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                    var user1 = new User { Email = model.Email, Password = model.Password, UserName = model.UserName, Role = role };
                    user1.LicenseKey = Guid.NewGuid().ToString();
                    db.Users.Add(user1);
                    await db.SaveChangesAsync();

                    await Authenticate(user1); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Пользователь с данным именем/почтой уже существует");
            }
            return View(model);
        }


        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}