using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using CzSharp.Model.Entities;
using CzSharp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace CzSharp.Controllers
{
    public class UsersController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILogger<UsersController> logger;
        
        public UsersController(SignInManager<User> signInManager, 
            UserManager<User> userManager, 
            ILogger<UsersController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var result = await signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password,
                loginViewModel.RememberMe, true);
            Debug.WriteLine("Logging in");

            if (result.Succeeded)
            {
                return Redirect("/");
            }
            
            ModelState.AddModelError("err", "Chybně zadané uživatelské jméno nebo heslo.");
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(RegistrationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = viewModel.Username,
                    Email = viewModel.Email
                };
                var result = await userManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }

                foreach (var error in result.Errors)
                {
                    if (error.Code == "DuplicateUserName")
                    {
                        ModelState.AddModelError<RegistrationViewModel>(r => r.Username, error.Description);
                    }
                    else if (error.Code == "DuplicateEmail")
                    {
                        ModelState.AddModelError<RegistrationViewModel>(r => r.Email, error.Description);
                    }
                    
                    return View(viewModel);
                }
               
            }
            
            ModelState.AddModelError("", "Vyplňte prosím všechny údaje.");
            return View(viewModel);
        }
    }
}