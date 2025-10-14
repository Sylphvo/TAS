using Microsoft.AspNetCore.Mvc;

namespace TAS.Controllers
{
    public class InformationGardenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
