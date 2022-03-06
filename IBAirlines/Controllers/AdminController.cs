using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace IBAirlines.Controllers
{
    public class AdminController : Controller
    {
        private readonly IConfiguration configuration;

        public AdminController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            Console.WriteLine(HttpContext.User);

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Pilotes/");
            }
            
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password, string ReturnUrl)
        {
            var credentials = configuration.GetSection("Auth");

            if (username == credentials["AdminLogin"] && password == credentials["AdminPassword"])
            {
                var claims = new List<Claim>
                 {
                    new Claim(ClaimTypes.Name, username)
                 };

                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new
                ClaimsPrincipal(claimsIdentity));
                return Redirect(ReturnUrl == null ? "/Pilotes/" : ReturnUrl);
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("/Admin");
        }
    }
}
