using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CzSharp.Model.Entities;
using CzSharp.Services;
using CzSharp.Utils.Extensions;
using CzSharp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Shows page for login
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Shows page for registration
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();
        }

        /// <summary>
        /// Logs user in by loginViewModel params
        /// </summary>
        /// <param name="loginViewModel">Parameters for login</param>
        /// <returns></returns>
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

        /// <summary>
        /// Logs user out
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("/");
        }

        /// <summary>
        /// Creates new account specified by params
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
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
                var generationToken = await usersManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Users", new {userId = user.Id, token = generationToken}, "https", "czsharp.net");
                await emailSender.SendEmailAsync(user.Email, "Confirm your account",  
                    $"Please confirm your account by clicking this link: <a href='{url}'>link</a>");

                if (result.Succeeded)
                {
                    TempData.AddSuccessMessage("Váš účet byl vytvořen a na váš email byl odeslán email s aktivací, po potvrzení se budete moci přihlásit.");
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

        /// <summary>
        /// Confirms users email
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await usersManager.FindByIdAsync(userId);
            var result = await usersManager.ConfirmEmailAsync(user, token);
            

            if (result.Succeeded)
            {
                TempData.AddSuccessMessage("Tvůj účet byl aktivován, nyní se můžeš příhlásit.");
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("Login");
            }
        }

        /// <summary>
        /// Shows user detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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