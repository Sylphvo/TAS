using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TAS.Data;
using TAS.Models;

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
		public IActionResult Login()
		{
			return View();
		}
		// Register
		public IActionResult Register()
		{
            return View();
        }
		// ConfirmEmail
		public IActionResult ConfirmEmail()
		{
            return View();
        }
		// ForgotPassword
		public IActionResult ForgotPassword()
		{
            return View();
        }
		// ResetPassword
		public IActionResult ResetPassword()
		{
            return View();
        }
		// Logout
		public IActionResult Logout()
		{
            return View();
        }
		#endregion

		#region Call Models
		[HttpPost]
		public IActionResult Login([FromBody] LoginRequest res)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var token = "jwt-token";
			return Json(new { success = true, token }); // Content-Type: application/json
		}
		#endregion

	}
}
