﻿using Microsoft.AspNetCore.Mvc;
using VoltesTechnology.Models.DTO;
using VoltesTechnology.Repositories.Abstract;
using VoltesTechnology.Repositories.Implementation;

namespace VoltesTechnology.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private IUserAuthenticationService authService;

        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            this.authService = authService;
        }

        /*public async Task<IActionResult> Register()
        {
            var model = new RegistrationModel
            {
                Email = "admin@gmail.com",
                Username = "admin",
                Name = "Voltes",
                Password = "Admin@123",
                PasswordConfirm = "Admin@123",
                Role = "Admin"
            };

            var result = await authService.RegisterAsync(model);
            return Ok(result.Message);
        }*/

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await authService.LoginAsync(model);

            if(result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = "Could not log in";
                return RedirectToAction(nameof(Login));
            }
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
