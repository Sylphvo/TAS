using Microsoft.AspNetCore.Mvc;
using TAS.ViewModels;

namespace TAS.Controllers
{
    public class TraceabilityController : Controller
    {
		TraceabilityModels models;
		CommonModels commonModels;
		public TraceabilityController()
		{
			models = new TraceabilityModels();
			commonModels = new CommonModels();
		}
		public IActionResult Traceability()
        {
            return View();
        }
		#region handle Data
		[HttpPost]
		public async Task<JsonResult> Traceabilitys()
		{
			var lstData = await models.GetTraceabilityAsync();

			return new JsonResult(lstData);
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
