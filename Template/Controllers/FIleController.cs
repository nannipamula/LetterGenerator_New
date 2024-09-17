using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Login(string userName, string passWord)
        {
            // Hardcoded credentials (for demo purposes)
            string hardcodedUserName = "admin";
            string hardcodedPassword = "123";

            // Check if the credentials are correct
            if (userName == hardcodedUserName && passWord == hardcodedPassword)
            {
                // Redirect to Index if the login is successful
                return RedirectToAction("LetterGenerate", "Home");
            }

            // If login fails, set an error message and return to login view
            ViewBag.ErrorMessage = "Invalid username or password!";
            return View();
        }


        
    }
}
