using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TAS.Data;
using TAS.Helpers;
using TAS.TagHelpers;

namespace TAS.Controllers
{
	public class AccountController : Controller
	{
		private readonly ApplicationDbContext _db;

		public AccountController(ApplicationDbContext context)
		{
            _db = context;
		}
		#region Views

		// ConfirmEmail
		[AllowAnonymous]
		public IActionResult ConfirmEmail()
		{
            return View();
        }
		// ForgotPassword
		[AllowAnonymous]
		public IActionResult ForgotPassword()
		{
            return View();
        }
		// ResetPassword
		[AllowAnonymous]
		public IActionResult ResetPassword()
		{
            return View();
        }

        #endregion

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() => View();

        [HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(UserAccount userLogin)
		{
            var user = await _db.Users
           .FirstOrDefaultAsync(x => x.UserName == userLogin.UserName || x.Email == userLogin.Email);

            if (user == null || !user.IsActive || !PasswordHelper.Verify(userLogin.Password, user.PasswordHash))
            {
                ModelState.AddModelError("", "Sai thông tin đăng nhập");
                return View(userLogin);
            }

            user.LogIn = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };
            var id = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id),
                new AuthenticationProperties { IsPersistent = userLogin.RememberMe });

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() => View();
        [HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(UserAccount userRegister)
		{
            //if (!ModelState.IsValid) return View(userRegister);

            bool exists = await _db.Users
                .AnyAsync(x => x.Email == userRegister.Email || x.UserName == userRegister.UserName);
            if (exists)
            {
                ModelState.AddModelError("", "Email hoặc UserName đã tồn tại");
                return View(userRegister);
            }

            var user = new UserAccount
            {
                UserId = Guid.NewGuid(),
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                UserName = userRegister.UserName,
                Email = userRegister.Email,
                PasswordHash = PasswordHelper.Hash(userRegister.Password),
                AcceptTerms = userRegister.AcceptTerms,
                IsActive = true,
                TwoFactorEnabled = false
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Login));
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			HttpContext.Session.Clear();
			return RedirectToAction("Login", "Account");
		}

	}
}
