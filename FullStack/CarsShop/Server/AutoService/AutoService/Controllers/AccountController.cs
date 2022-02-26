using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoService.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Controllers
{
    public class AccountController : Controller
    {
        private readonly AutoServiceContext context;
        public AccountController(AutoServiceContext context)
        {
            this.context = context;
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
                User user = await context.Users.Include(n => n.Role)
                    .FirstOrDefaultAsync(u => u.Lgn == model.Login && u.Password == model.Password);

                if (user != null)
                {
                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
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
                User user = await context.Users.FirstOrDefaultAsync(u => u.Lgn == model.Login);
                if (user == null)
                {
                    user = new User {Lgn = model.Login, Password = model.Password};
                    Role userRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "User");
                    if (userRole != null)
                        user.Role = userRole;

                    context.Users.Add(user);
                    await context.SaveChangesAsync();

                    await Authenticate(user); 

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Lgn),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
