﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VoteForCauseFinal.Models.ViewModels;

namespace VoteForCauseFinal.Controllers
{
    public class AccountControllercs : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;


        //injecting the user manager
        //injecting the sign in manager
        public AccountControllercs(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        
        


        [HttpGet]
        public IActionResult Register()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel) 
        {
            if(ModelState.IsValid)
            {

                //create new user
                var identityUser = new IdentityUser
                {
                    UserName = registerViewModel.Username,
                    Email = registerViewModel.Email
                };

                var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

                if (identityResult.Succeeded)
                {
                    //assign this user the 'User' role
                    var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");


                    if (roleIdentityResult.Succeeded)
                    {
                        //show success 
                        return RedirectToAction("Register");
                    }


                }
            }

            //show error
            return View();
        
        }




        [HttpGet]
        public IActionResult Login(string ReturnUrl) 
        {

            var model = new LoginViewModel 
            { 
                ReturnUrl = ReturnUrl 
            };

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login (LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

           var signInResult = await signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);
            
            if(signInResult != null && signInResult.Succeeded)
            {
                if(!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }



                return RedirectToAction("Index", "Home");
            }

            //show errors
            return View();


        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            //index method in the home controller
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
