using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TAS.Data;
using TAS.Models;
using TAS.TagHelpers;

namespace TAS.Controllers
{
	public class AccountController : Controller
	{
		private readonly ApplicationDbContext _context;

		public AccountController(ApplicationDbContext context)
		{
			_context = context;
		}
		#region Views
		// Login
		[AllowAnonymous]
		public IActionResult Login(string? returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View(new LoginRequest());
		}

		// Register
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register(string? returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View(new RegisterRequest());
		}

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

		#region Call Models
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginRequest input, string? returnUrl = null)
		{
			if (!ModelState.IsValid) return View(input);

			var normalized = input.Email.Trim().ToUpperInvariant();

			// Truy vấn bằng shadow property "NormalizedEmail"
			var user = await _context.Set<UserAccount>()
				.SingleOrDefaultAsync(x => EF.Property<string>(x, "NormalizedEmail") == normalized);

			// Không lộ thông tin tồn tại tài khoản
			if (user is null || !user.IsActive || IsLocked(user))
			{
				await FailAsync(user);
				ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng.");
				return View(input);
			}

			var ok = PasswordHelper.Verify(input.Password, user.PasswordSalt, user.HashIter, user.PasswordHash);
			if (!ok)
			{
				await FailAsync(user);
				ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng.");
				return View(input);
			}

			// Reset đếm lỗi và lockout
			user.FailedAccessCount = 0;
			user.LockoutEnd = null;
			await _context.SaveChangesAsync();

			// Tạo cookie đăng nhập
			var claims = new List<Claim>
		{
			new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
			new(ClaimTypes.Name, user.Email)
            // thêm role nếu có: new(ClaimTypes.Role, "Admin")
        };
			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(identity);
			var props = new AuthenticationProperties
			{
				IsPersistent = input.RememberMe,
				AllowRefresh = true,
				ExpiresUtc = DateTimeOffset.UtcNow.AddDays(14)
			};
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);

			if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterRequest input, string? returnUrl = null)
		{
			if (!input.AcceptTerms) ModelState.AddModelError(nameof(input.AcceptTerms), "Bạn phải đồng ý điều khoản.");
			if (!ModelState.IsValid) return View(input);

			var normalized = input.Email.Trim().ToUpperInvariant();

			var exists = await _context.Set<UserAccount>()
				.AnyAsync(x => EF.Property<string>(x, "NormalizedEmail") == normalized);
			if (exists)
			{
				ModelState.AddModelError(nameof(input.Email), "Email đã tồn tại.");
				return View(input);
			}

			var (hash, salt, iter) = PasswordHelper.Hash(input.Password);

			var user = new UserAccount
			{
				Email = input.Email.Trim(),
				PasswordHash = hash,
				PasswordSalt = salt,
				HashIter = iter,
				HashAlgo = "PBKDF2",
				IsActive = true,
				FailedAccessCount = 0,
				TwoFactorEnabled = false
			};

			_context.Add(user);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				// tranh chấp duy nhất
				ModelState.AddModelError(nameof(input.Email), "Email đã tồn tại.");
				return View(input);
			}

			// Đăng nhập ngay sau khi đăng ký
			var claims = new List<Claim>
		{
			new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
			new(ClaimTypes.Name, user.Email)
		};
			var id = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(id),
				new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTimeOffset.UtcNow.AddDays(14) });

			if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Account");
		}

		private static bool IsLocked(UserAccount u) =>
			u.LockoutEnd is not null && u.LockoutEnd > DateTime.UtcNow;

		private async Task FailAsync(UserAccount? u)
		{
			if (u is null) return;
			u.FailedAccessCount++;
			if (u.FailedAccessCount >= 5)
			{
				u.LockoutEnd = DateTime.UtcNow.AddMinutes(15);
				u.FailedAccessCount = 0;
			}
			await _context.SaveChangesAsync();
		}
		#endregion

	}
}
