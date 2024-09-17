using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System;

namespace Template.Controllers
{
    public class FIleController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // This is a simple example, replace with proper user validation logic
            if (username == "ElixirUser" && password == "LGPass123!")
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

                var claimsIdentity = new ClaimsIdentity(claims, "SessionAuthScheme");

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                };

                HttpContext.SignInAsync("SessionAuthScheme", new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("LetterGenerate", "Home");
            }

            ViewBag.ErrorMessage = "Invalid credentials";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("SessionAuthScheme");
            return RedirectToAction("Login", "File");
        }



    }
}
