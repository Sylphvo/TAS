using Microsoft.AspNetCore.Mvc;

namespace TAS.Controllers
{
    public class RubberGardenController : Controller
    {
        public IActionResult RubberGarden()
        {
            return View();
        }
    }
}
