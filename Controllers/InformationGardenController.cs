using Microsoft.AspNetCore.Mvc;
using TAS.ViewModels;

namespace TAS.Controllers
{
    public class InformationGardenController : Controller
    {
		InformationGardenModels models;
		public InformationGardenController()
		{
			models = new InformationGardenModels();
		}

		public IActionResult InformationGarden()
        {
            return View();
        }
		#region handle Data
		[HttpPost]
        public async Task<JsonResult> InformationGardens()
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
