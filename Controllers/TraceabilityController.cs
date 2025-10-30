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
		private readonly IStringLocalizer<SharedResource> _localizer;
		public TraceabilityController(IStringLocalizer<SharedResource> localizer)
		{
			models = new TraceabilityModels();
			commonModels = new CommonModels();
			_localizer = localizer;
		}

		public IActionResult Traceability()
        {
			ViewBag.langTraceability = CultureInfo.CurrentUICulture.Name;
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
