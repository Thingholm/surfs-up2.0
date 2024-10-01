using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;
using Web.Models;
using Web.Controllers;

namespace Web
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Username!, model.Password!, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login attempt");
                return View(model);
            }
            return View(model);
        }

         public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
         public async Task<IActionResult> Register(RegisterVM model)
        {
            if(ModelState.IsValid)
            {
                User user = new User
                {
                   Name = model.Name,
                   UserName = model.Email,
                   Email = model.Email,
                   Address = model.Address 
                };

                var result = await userManager.CreateAsync(user, model.Password!);  

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

         public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
    
            return RedirectToAction("Index", "Home");
            
        }
    }
}