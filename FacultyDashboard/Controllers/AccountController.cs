using FacultyDashboard.Models;
using Common.Utilities;
using Data.Repositories;
using Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;

namespace FacultyDashboard.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<AccountController> logger;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(IUserRepository userRepository, ILogger<AccountController> logger,
            UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            this.userRepository = userRepository;
            this.logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        [TempData]
        public string ErrorMessage { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        //public string ReturnUrl { get; set; }
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            UserLoginDto Model = new UserLoginDto();
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            //ReturnUrl = returnUrl;
            Model.ReturnUrl = returnUrl;
            return View(Model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto Input, string returnUrl = null)
        {
            if (Input != null)
                Input.ReturnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var userName = Input.UserName;
                //if (IsValidEmail(Input.Email))
                //{
                //var user = await _userManager.FindByEmailAsync("admin@site.com"/*Input.Email*/);
                //var user = await userManager.FindByNameAsync(Input.UserName);
                //if (user != null)
                //{
                //    userName = user.UserName;
                //}
                //}
                //var passwordHash = SecurityHelper.GetSha256Hash(Input.Password);

                var result = await signInManager.PasswordSignInAsync(userName/*"admin@admin.com"*/, /*passwordHash*/Input.Password,
                    Input.RememberMe, lockoutOnFailure: false);

                //var result4 = await userManager.CheckPasswordAsync(user, passwordHash);

                if (result.Succeeded)
                {
                    string role = "";
                    var user = await userManager.FindByNameAsync(Input.UserName);
                    if (user != null)
                    {
                        var roles = await userManager.GetRolesAsync(user);
                        if (roles != null)
                        {
                            if (roles.Count > 0 && roles.Any(x => x.Contains("admin")))
                                role = "admin";
                            //else if (roles.Count > 0 && roles.Any(x => x.Contains("user")))
                            //    role = "user";
                        }
                    }
                    ClaimsIdentity identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, userName),
                        new Claim(ClaimTypes.Role, role)
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                    //bool isAuthenticated = false;
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                    return LocalRedirect(returnUrl);
                }
                else if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                else if (result.IsLockedOut)
                {
                    //_logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(Input);
                }
            }
            return View(Input);
        }
        public async Task<IActionResult> LoginPartial(string UserName, string Password/*UserLoginDto Input*/)
        {
            //if (Input != null)
            //    Input.ReturnUrl = returnUrl ?? Url.Content("~/Tutorial/Index");

            if (ModelState.IsValid)
            {
                var userName = UserName;
                var passwordHash = SecurityHelper.GetSha256Hash(Password);

                var result = await signInManager.PasswordSignInAsync(userName/*"admin@admin.com"*/,
                    /*passwordHash*/Password, true, lockoutOnFailure: false);

                //var result4 = await userManager.CheckPasswordAsync(user, passwordHash);

                if (result.Succeeded)
                {
                    string role = "";
                    var user = await userManager.FindByNameAsync(UserName);
                    if (user != null)
                    {
                        var roles = await userManager.GetRolesAsync(user);
                        if (roles != null)
                        {
                            if (roles.Count > 0 && roles.Any(x => x.Contains("admin")))
                                role = "admin";
                            //else if (roles.Count > 0 && roles.Any(x => x.Contains("user")))
                            //    role = "user";
                        }
                    }
                    ClaimsIdentity identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, userName),
                        new Claim(ClaimTypes.Role, role)
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                    //bool isAuthenticated = false;
                    var principal = new ClaimsPrincipal(identity);
                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                    //return LocalRedirect(returnUrl);
                }
                //else if (result.RequiresTwoFactor)
                //{
                //    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                //}
                else if (result.IsLockedOut)
                {
                    //_logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return new JsonResult(new { result = "ok" });
                }
            }
            return new JsonResult(new { result = "ok" });
        }
        [HttpGet]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            UserRegisterDto Model = new UserRegisterDto();
            if (returnUrl != null)
                Model.ReturnUrl = returnUrl;
            //ReturnUrl = returnUrl;
            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View(Model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto Input, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            //if (ModelState.IsValid)
            {
                Random random = new Random();
                MailAddress address = new MailAddress(Input.Email);
                string userName = address.User;
                var user = new User
                {
                    UserName = Input.UserName,// userName,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Phone = string.Format("{0:0000000}", random.Next(1, 1000000)),
                    IsActive = true
                };
                var result = await userManager.CreateAsync(user, Input.Password);
                if (result.Errors.Any(x => x.Code == "DuplicateEmail"))
                {
                    ModelState.AddModelError(string.Empty, "لطفا ایمیل دیگری وارد نمایید");
                    return View(Input);
                }
                if (result.Errors.Any(x => x.Code == "DuplicateUserName"))
                {
                    ModelState.AddModelError(string.Empty, "لطفا نام کاربری دیگری وارد نمایید");
                    return View(Input);
                }

                if (result.Succeeded)
                {
                    logger.LogInformation("User created a new account with password.");
                    await userManager.AddToRoleAsync(user, "admin");
                    //var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(Input);
        }
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await signInManager.SignOutAsync();
            //FormsAuthentication.SignOut();
            var prop = new AuthenticationProperties()
            {
                RedirectUri = returnUrl
            };
            // after signout this will redirect to your provided target
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, prop);
            HttpContext.Response.Cookies.Delete("UserLoginCookie");
            return Redirect("~/");
            //if (returnUrl != null)
            //{
            //    return LocalRedirect(returnUrl);
            //}
            //else
            //{
            //    return RedirectToPage();
            //}
            //return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}