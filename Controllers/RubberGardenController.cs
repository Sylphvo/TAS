using Microsoft.AspNetCore.Mvc;
using TAS.ViewModels;

namespace TAS.Controllers
{
    public class RubberGardenController : Controller
    {
		RubberGardenModels models = new RubberGardenModels();
		public IActionResult RubberGarden()
		{
			return View();
		}

		#region handle Data
		[HttpPost]
        public async Task<IActionResult> RubberGardens()
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
		#endregion
	}
}
