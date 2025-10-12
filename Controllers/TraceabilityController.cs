using Microsoft.AspNetCore.Mvc;

namespace TAS.Controllers
{
    public class TraceabilityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
