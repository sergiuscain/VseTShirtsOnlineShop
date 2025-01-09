using Microsoft.AspNetCore.Mvc;
using VseTShirts.Models;
using VseTShirts.DB.Models;
using VseTShirts.DB;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace VseTShirts.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<User> _usersManager;
        //private readonly IUsersManager  usersManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;
        public AccountController(UserManager<User> usersManager, SignInManager<User> signInManager, IAuthenticationSchemeProvider authenticationSchemeProvider)
        {
           // this.usersManager = usersManager;
            _signInManager = signInManager;
            _usersManager = usersManager;
            _authenticationSchemeProvider = authenticationSchemeProvider;
        }
        [Authorize]
        public IActionResult Index()
        {
            var user = _usersManager.FindByNameAsync(User.Identity.Name).Result;
            var userVM = user.ToViewModel();
            return View(userVM);
        }
        public async Task<IActionResult> Login(string returnUrl)
        {
           
            var externalSchemes = (await _authenticationSchemeProvider.GetAllSchemesAsync()).Where(s => s.DisplayName != null).ToList();
            return View(new LoginModel() { ReturnUrl = returnUrl, ExternalLogins = externalSchemes });
        }

        [HttpPost]
        public IActionResult LoginAsync( LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(login.UserName, login.Password, login.isRemembMe, false).Result;
                if (result.Succeeded)
                {
                    return Redirect(login.ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                }
            }
                return View(login);
        }



        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterModel() {ReturnUrl = returnUrl});
        }

        [HttpPost]
        public IActionResult RegisterAsync(RegisterModel register)
        {
            if (register.UserName == register.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не могут совпадать");
            }
            if (ModelState.IsValid)
            {
               User user = new User 
               {
                   Email = register.Email,
                   UserName = register.UserName,
                   PhoneNumber = register.PhoneNumber,
                   Role = Constants.UserRoleName,
                   AvatarURL = "/Images/Avatar/standart.png",
                   FirstName = register.FirstName,
                   LastName = register.LastName,
                   Gender = register.Gender.ToString(),
                   Status = UserStatus.Active.ToString(),
                   DateOfBirth = register.DateOfBirth
               };
               var result = _usersManager.CreateAsync(user, register.Password).Result;
                if (result.Succeeded)
                {
                    _signInManager .SignInAsync(user, false).Wait();
                    try
                    {
                        _usersManager.AddToRoleAsync(user, Constants.UserRoleName).Wait();
                    }
                    catch (System.Exception ex)
                    {
                        ModelState.AddModelError("", "Не удалось добавить роль пользователю: " + ex.Message);
                    }
                    return Redirect(register.ReturnUrl?? "/Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }
            return View(register);
        }
        public  IActionResult Logout()
        {
             _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Index");
        }

        public IActionResult ResetPassword()
        {
            return View();
        }
        public IActionResult HelloNewUser(string defaultPassword)
        {
            return View(new {defaultPassword });
        }

    }
}
