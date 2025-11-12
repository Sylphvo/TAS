using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;
using TAS.Resources;
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
		#region Traceability
		public IActionResult Traceability()
        {
			return View();
        }
		#region handle Data Traceabilitys
		[HttpPost]
		public async Task<JsonResult> Traceabilitys()
		{
			var lstData = await models.GetTraceabilityAsync();

			return new JsonResult(lstData);
		}
		[HttpPost("AddOrUpdateTraceability")]
		public IActionResult AddOrUpdateTraceability()
		{
			return View();
		}
		[HttpPost("DeleteTraceability/{id}")]
		public IActionResult Delete(int id)
		{
			return View();
		}
		#endregion
		#endregion

		#region handle Data Pallets
		[HttpPost]
		public async Task<JsonResult> GetPallets(int orderId)
		{
			var lstData = await models.GetPallets();
			return new JsonResult(lstData);
		}
		[HttpPost("AddOrUpdatePallet")]
		public IActionResult AddOrUpdatePallets()
		{
			return View();
		}
		[HttpPost("DeletePallet/{id}")]
		public IActionResult DeletePallets(int id)
		{
			return View();
		}
		#endregion
	}
}
