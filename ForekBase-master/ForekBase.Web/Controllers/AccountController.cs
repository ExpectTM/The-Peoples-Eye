//     Copyright © Forek ICT.
// </copyright>
// Created By:      IF Oliphant (on IFOliphantPC)
// Created Date:    09/Jan/2024 21:00 PM
// Purpose:         Defines the Account Controller.


#region Using Directives

using ForekBase.Application.Common.Interfaces;
using ForekBase.Application.Common.Utility;
using ForekBase.Domain.Entities;
using ForekBase.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace ForekBase.Web.Controllers
{
    /// <summary>
    /// Controller for managing user accounts, including login, registration, and logout.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for coordinating operations on the database.</param>
        /// <param name="userManager">The user manager for managing user-related operations.</param>
        /// <param name="signInManager">The sign-in manager for managing user sign-in operations.</param>
        /// <param name="roleManager">The role manager for managing user roles.</param>
        public AccountController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Displays the login view.
        /// </summary>
        /// <param name="returnUrl">The URL to redirect to after a successful login.</param>
        /// <returns>The login view.</returns>
        public IActionResult Login(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            LoginVM loginVM = new()
            {
                RedirectUrl = returnUrl,
            };

            return View(loginVM);
        }

        /// <summary>
        /// Handles the HTTP POST request for user login.
        /// </summary>
        /// <param name="loginVM">The login view model containing user credentials.</param>
        /// <returns>Redirects to the specified URL after successful login; otherwise, returns the login view with an error message.</returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, loginVM.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVM.RedirectUrl))
                    {
                        return RedirectToAction("Whatever", "Wherever");
                    }
                    else
                    {
                        return LocalRedirect(loginVM.RedirectUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login Attempt");
                }
            }

            return View(loginVM);
        }

        /// <summary>
        /// Displays the registration view.
        /// </summary>
        /// <returns>The registration view.</returns>
        public IActionResult Register()
        {
            if (!_roleManager.RoleExistsAsync(Permissions.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(Permissions.Role_Admin)).Wait();
                _roleManager.CreateAsync(new IdentityRole(Permissions.Role_SuperAdmin)).Wait();
            }

            RegisterVM registerVM = new()
            {
                RoleList = _roleManager.Roles.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id
                })
            };

            return View(registerVM);
        }

        /// <summary>
        /// Handles the HTTP POST request for user registration.
        /// </summary>
        /// <param name="registerVM">The registration view model containing user information.</param>
        /// <returns>Redirects to the specified URL after successful registration; otherwise, returns the registration view with an error message.</returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            ApplicationUser user = new()
            {
                Name = registerVM.Name,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber,
                NormalizedEmail = registerVM.Email.ToUpper(),
                EmailConfirmed = true,
                UserName = registerVM.Email,
                CreatedOn = DateTime.Now,
            };

            var result = await _userManager.CreateAsync(user, registerVM.Password);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(registerVM.Role))
                {
                    await _userManager.AddToRoleAsync(user, registerVM.Role);
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, Permissions.Role_Unknown);
                }

                await _signInManager.SignInAsync(user, isPersistent: false);

                if (string.IsNullOrEmpty(registerVM.RedirectUrl))
                {
                    return RedirectToAction("Whatever", "Wherever");
                }
                else
                {
                    return LocalRedirect(registerVM.RedirectUrl);
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            registerVM.RoleList = _roleManager.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id
            });

            return View(registerVM);
        }

        /// <summary>
        /// Logs the user out and redirects to a specific action.
        /// </summary>
        /// <returns>Redirects to the specified action after successful logout.</returns>
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Whatever", "Whenever");
        }
    }
}
