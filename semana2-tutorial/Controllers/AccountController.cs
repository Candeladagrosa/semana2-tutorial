using Microsoft.AspNetCore.Mvc;
using semana2_tutorial.Models;

namespace semana2_tutorial.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                // Validación básica de usuario (hardcodeado)
                if (user.Username == "admin" && user.Password == "1234")
                {
                    HttpContext.Session.SetString("Username", user.Username);
                    TempData["Mensaje"] = "¡Bienvenido a nuestra web, " + user.Username + "!";
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            }

            return View(user);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
