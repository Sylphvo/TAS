using Microsoft.AspNetCore.Mvc;
using TAS.ViewModels;
using static Azure.Core.HttpHeader;

namespace TAS.Controllers
{
    public class AgentController : Controller
    {
		private readonly AgentModels models;
		private readonly CommonModels _common;
		public AgentController(AgentModels _models, CommonModels common)
		{
			models = _models;
			_common = common;
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
