using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CzSharp.Model.Entities;
using CzSharp.Services;
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
        private readonly UserManager<User> usersManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILogger<UsersController> logger;
        private readonly IEmailSender emailSender;
        
        public UsersController(SignInManager<User> signInManager, 
            UserManager<User> usersManager, 
            ILogger<UsersController> logger,
            IEmailSender emailSender)
        {
            this.usersManager = usersManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
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

        [HttpGet]
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
                var result = await usersManager.CreateAsync(user, viewModel.Password);
                var generationToken = usersManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Users", new {userId = user.Id, token = generationToken});
                await emailSender.SendEmailAsync(user.Email, "Confirm your account",  
                    $"Please confirm your account by clicking this link: <a href='{url}'>link</a>");

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

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await usersManager.FindByIdAsync(userId);
            var result = await usersManager.ConfirmEmailAsync(user, token);
            

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Tvůj účet byl aktivován, nyní se můžeš příhlásit.";
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id = null)
        {
            User user = null;
            if (id != null)
            {
                user = await usersManager.FindByIdAsync(id);
            }
            else if (User.Identity.IsAuthenticated)
            {
                user = await usersManager.GetUserAsync(User);
            }
            
            if (user != null)
            {
                return View(new UserWithRolesViewModel(user, (await usersManager.GetRolesAsync(user)).ToList()));
            }
            else
            {
                return NotFound("User was not found");
            }
        }
    }
}