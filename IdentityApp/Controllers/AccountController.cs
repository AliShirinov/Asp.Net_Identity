using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRApp.Models;
using HRApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IPasswordValidator<User> _passwordValidator;


        public AccountController(UserManager<User> userManager
                                , RoleManager<IdentityRole> roleManager
                                , SignInManager<User> signInManager
                                , IPasswordHasher<User> passwordHasher
                                , IPasswordValidator<User> passwordValidator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                User currentUser = await _userManager.FindByEmailAsync(registerModel.Email);
                {
                    if (currentUser != null)
                    {
                        ModelState.AddModelError("", "This user already exist");
                        return View();
                    }
                    else
                    {
                        User user = new User
                        {
                            Name = registerModel.Name,
                            Surname = registerModel.Surname,
                            Email = registerModel.Email,
                            UserName = registerModel.Surname + registerModel.Name

                        };

                        user.PasswordHash = _passwordHasher.HashPassword(user, registerModel.Password);

                        IdentityResult userCreateResult = await _userManager.CreateAsync(user);
                        if (userCreateResult.Succeeded)
                        {
                            return (RedirectToAction(nameof(Login)));
                        }
                        else
                        {
                            foreach (IdentityError error in userCreateResult.Errors)
                            {
                                ModelState.AddModelError("", error.Description);

                            }
                            return View();
                        }
                    }


                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                User currentUser = await _userManager.FindByEmailAsync(loginModel.Email);
                if (currentUser==null)
                {
                    ModelState.AddModelError("", "Given user not exist");
                    return View();
                }
                
                    IdentityResult passwordValidationResult = await _passwordValidator
                                              .ValidateAsync(_userManager
                                                              , currentUser
                                                              , loginModel.Password);
                if (passwordValidationResult.Succeeded)
                {
                    await _signInManager.SignInAsync(currentUser, true);
                    return RedirectToAction(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Password is not valid");
                    return View();
                }
                
            }


            return View();
        }

        [HttpGet]
        public IActionResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(UserPasswordModel passwordModel)
        {
            if (ModelState.IsValid)
            {
                User cUser = await _userManager.FindByEmailAsync(passwordModel.Email);
                if (cUser==null)
                {
                    ModelState.AddModelError("", "User not exist");
                    return View();
                }
                PasswordVerificationResult verificationResult = _passwordHasher.VerifyHashedPassword(cUser, cUser.PasswordHash, passwordModel.OldPassword);
                if (verificationResult == PasswordVerificationResult.Success)
                {
                    cUser.PasswordHash = _passwordHasher.HashPassword(cUser, passwordModel.NewPassword);
                    IdentityResult passwordUpdateResult = await _userManager.UpdateAsync(cUser);
                    if (!passwordUpdateResult.Succeeded)
                    {
                        ModelState.AddModelError("", "Password update failed");
                        return View();
                    }
                   
                }
                else
                {
                    ModelState.AddModelError("", "Old password is not valid");

                }
            }
            return View();
        }
    }
}