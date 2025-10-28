using Microsoft.AspNetCore.Mvc;
using TAS.ViewModels;

namespace TAS.Controllers
{
    public class RubberGardenController : Controller
    {
		RubberGardenModels models;
		CommonModels commonModels;
		public RubberGardenController()
		{
			models = new RubberGardenModels();
			commonModels = new CommonModels();
		}
		public async Task<IActionResult> RubberGardenAsync()
		{
			ViewBag.ComboAgent = await commonModels.ComboAgent();
			return View();
		}
		#region handle Data
		[HttpPost]
        public async Task<IActionResult> RubberGardens()
        {
			var lstData = await models.GetRubberFarmAsync();

			return new JsonResult(lstData);
		}
		[HttpPost]
		public async Task<IActionResult> ComboAgent()
		{
			var lstData = await models.GetRubberFarmAsync();
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
		public JsonResult GetAllCombo()
		{
			return Json(null);
		}
		#endregion
	}
}
