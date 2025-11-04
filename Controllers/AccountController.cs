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
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.GivenName, user.FirstName ?? ""), // FirstName
				new Claim(ClaimTypes.Surname,   user.LastName  ?? ""), // LastName
				new Claim("FullName", $"{user.FirstName} {user.LastName}".Trim())
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

            bool exists = await _db.Users.AnyAsync(x => x.Email == userRegister.Email || x.UserName == userRegister.UserName);
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
		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Account");
		}
		//// Upload avatar
		//[HttpPost("users/{id:guid}/avatar")]
		//public async Task<IActionResult> UploadAvatar(Guid id, IFormFile file, [FromServices] IWebHostEnvironment env)
		//{
		//	if (file == null || file.Length == 0) return BadRequest("Empty file");
		//	var allowed = new[] { "image/jpeg", "image/png", "image/webp", "image/gif" };
		//	if (!allowed.Contains(file.ContentType)) return BadRequest("Invalid type");
		//	const long MAX = 2 * 1024 * 1024; // 2MB
		//	if (file.Length > MAX) return BadRequest("Too large");

		//	var user = await _db.Set<UserAccount>().FindAsync(id);
		//	if (user == null) return NotFound();

		//	var ext = Path.GetExtension(file.FileName);
		//	var fileName = $"{Guid.NewGuid()}{ext}";
		//	var folder = Path.Combine(env.WebRootPath, "uploads", "avatars");
		//	Directory.CreateDirectory(folder);
		//	var path = Path.Combine(folder, fileName);

		//	// xóa file cũ nếu có
		//	if (!string.IsNullOrWhiteSpace(user.AvatarUrl))
		//	{
		//		var oldPath = Path.Combine(env.WebRootPath, user.AvatarUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
		//		if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
		//	}

		//	using (var fs = System.IO.File.Create(path))
		//		await file.CopyToAsync(fs);

		//	user.AvatarUrl = $"/uploads/avatars/{fileName}";
		//	user.AvatarUpdatedAt = DateTime.UtcNow;
		//	await _db.SaveChangesAsync();

		//	// trả về URL kèm phiên bản để tránh cache
		//	return Ok(new { url = $"{user.AvatarUrl}?v={user.AvatarUpdatedAt!.Value.Ticks}" });
		//}
	}
}
