using Microsoft.AspNetCore.Mvc;
using TAS.Models;
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
		public JsonResult AddOrUpdate(RubberIntakeRequest rubberIntakeRequest)
		{
			int result = models.AddOrUpdateRubber(rubberIntakeRequest);
			return Json(result);
		}

		[HttpPost]
		public JsonResult ImportDataLstData([FromBody] List<RubberIntakeRequest> rowsData)
		{
			int result = models.ImportListData(rowsData);
			return Json(result);
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
