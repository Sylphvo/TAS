using Microsoft.AspNetCore.Mvc;

namespace TAS.Controllers
{
    public class AgentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
