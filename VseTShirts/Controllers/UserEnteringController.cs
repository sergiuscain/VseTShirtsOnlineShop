using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Serilog.Core;
using System.Security.Claims;
using VseTShirts.DB.Models;
using VseTShirts.Helpers;
using VseTShirts.Models;

namespace VseTShirts.Controllers
{
    public class UserEnteringController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _usersManager;
        public UserEnteringController(SignInManager<User> signInManager, UserManager<User> usersManager)
        {
            _signInManager = signInManager;
            _usersManager = usersManager;
        }
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallvack", "UserEntering", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }
        public async Task<IActionResult> ExternalLoginCallvack(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var loginViewModel = new LoginModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList(),
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Ошибка при авторизации: {remoteError}");
                return View("Login", loginViewModel);
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, $"Ошибка при загрузки данных от провайдера (метод GetExternalLoginInfoAsync)");
                return View("Login", loginViewModel);
            }
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false, false);
            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if(email != null)
                {
                    var user = await _usersManager.FindByEmailAsync(email);
                    var defaultPassword = string.Empty;
                    var isNewUser = false;
                    if (user == null)
                    {
                        user = new User
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            FirstName = info.Principal.FindFirstValue(ClaimTypes.Name).Split(" ")[0],
                            LastName = info.Principal.FindFirstValue(ClaimTypes.Name).Split(" ")[1],
                            PhoneNumber = info.Principal.FindFirstValue(ClaimTypes.MobilePhone),
                            AvatarURL = "/Images/Avatar/standart.png",
                            Status = UserStatus.Active.ToString(),
                            DateOfBirth = DateTime.Now,
                            Gender = "NoN",
                            Role = Data.UserRoleName.ToString(),
                        };
                        defaultPassword = $"@A1{Guid.NewGuid().ToString().Substring(0, 10)}";
                        isNewUser = true ;
                        await _usersManager.CreateAsync(user, defaultPassword);
                        await _usersManager.AddToRoleAsync(user, Data.UserRoleName);
                    }
                    await _usersManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, false);

                    if (isNewUser)
                    {
                        return RedirectToAction("HelloNewUser", "Account",new { defaultPassword });
                    }
                    return LocalRedirect(returnUrl);
                }
            }
            ViewBag.ErrorTitle = $"Email claim not received from {info.LoginProvider}";
            ViewBag.ErrorMessage = $"Please contact support on asfadfsdfgasdfg.support@gmail.com";
            return View("Error");
        }
    }
}