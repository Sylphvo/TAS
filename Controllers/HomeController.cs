using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TAS.Data;
using TAS.Helpers;

namespace TAS.Controllers
{
	public class HomeController : Controller
	{
		CommonController commonController = new CommonController();
		private readonly ApplicationDbContext _context;

		public HomeController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Products
		[Authorize]
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Layout()
		{
			return PartialView("_Layout");
		}
		public IActionResult Header()
		{
			return PartialView("_Header");
		}

	}
}
