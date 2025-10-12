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
		public async Task<IActionResult> Login()
		{
			var products = await _context.Products.ToListAsync();
			return View(products);
		}
		// Register
		public async Task<IActionResult> Register()
		{
			var products = await _context.Products.ToListAsync();
			return View(products);
		}
		// ConfirmEmail
		public async Task<IActionResult> ConfirmEmail()
		{
			var products = await _context.Products.ToListAsync();
			return View(products);
		}
		// ForgotPassword
		public async Task<IActionResult> ForgotPassword()
		{
			var products = await _context.Products.ToListAsync();
			return View(products);
		}
		// ResetPassword
		public async Task<IActionResult> ResetPassword()
		{
			var products = await _context.Products.ToListAsync();
			return View(products);
		}
		// Logout
		public async Task<IActionResult> Logout()
		{
			var products = await _context.Products.ToListAsync();
			return View(products);
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
