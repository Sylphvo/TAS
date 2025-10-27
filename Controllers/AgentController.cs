using Microsoft.AspNetCore.Mvc;
using TAS.ViewModels;

namespace TAS.Controllers
{
    public class AgentController : Controller
    {
		AgentModels models;
		public AgentController()
		{
			models = new AgentModels();
		}
		public IActionResult Agent()
        {
            return View();
        }
		#region handle Data
		[HttpPost]
		public async Task<IActionResult> Agents()
		{
			var lstData = await models.GetRubberAgentAsync();
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
