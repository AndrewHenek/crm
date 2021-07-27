using Microsoft.AspNetCore.Mvc;

namespace crm.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
