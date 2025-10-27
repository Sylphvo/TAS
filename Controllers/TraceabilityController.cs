using Microsoft.AspNetCore.Mvc;

namespace TAS.Controllers
{
    public class TraceabilityController : Controller
    {
        public IActionResult Traceability()
        {
            return View();
        }
		#region handle Data
		[HttpGet]
		public IActionResult Traceabilitys()
		{
			return View();
		}
		[HttpPost("AddOrUpdate")]
		public IActionResult AddOrUpdate()
		{
			return View();
		}
		[HttpPost("Delete/{id}")]
		public IActionResult Delete(int id)
		{
			return View();
		}
		#endregion
	}
}
