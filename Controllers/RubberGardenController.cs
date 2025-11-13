using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TAS.Models;
using TAS.ViewModels;

namespace TAS.Controllers
{
    public class RubberGardenController : Controller
    {
		private readonly RubberGardenModels _models;
		private readonly CommonModels _common;
		public RubberGardenController(RubberGardenModels models, CommonModels common)
		{
			_models = models;
			_common = common;
		}

		public async Task<IActionResult> RubberGardenAsync()
		{
			ViewBag.ComboAgent = await _common.ComboAgent();
			ViewBag.ComboFarmCode = await _common.ComboFarmCode();
			return View();
		}

		#region handle Data
		[HttpPost]
        public async Task<IActionResult> RubberGardens()
        {
			var lstData = await _models.GetRubberFarmAsync();

			return new JsonResult(lstData);
		}

		[HttpPost]
		public JsonResult AddOrUpdate([FromBody] RubberIntakeRequest rubberIntakeRequest)
		{
			int result = _models.AddOrUpdateRubber(rubberIntakeRequest);
			return Json(result);
		}
		[HttpPost]
		public JsonResult AddOrUpdateFull([FromBody] List<RubberIntakeRequest> lstRubberIntake)
		{		
			int result = _models.AddOrUpdateRubberFull(lstRubberIntake);
			return Json(1);
		}

		[HttpPost]
		public JsonResult ImportDataLstData([FromBody] List<RubberIntakeRequest> rowsData)
		{
			int result = _models.ImportListData(rowsData);
			return Json(result);
		}

		[HttpPost]
		public JsonResult ApproveDataRubber(int intakeId, int status)
		{
			int result = _models.ApproveDataRubber(intakeId, status);
			return Json(result);
		}

		[HttpPost]
		public JsonResult ApproveAllDataRubber(int status)
		{
			int result = _models.ApproveAllDataRubber(status);
			return Json(result);
		}

		[HttpPost]
		public JsonResult DeleteRubber(int intakeId)
		{
			return Json(_models.DeleteRubber(intakeId));
		}

		public JsonResult GetAllCombo()
		{
			return Json(null);
		}
		#endregion		
	}
}
