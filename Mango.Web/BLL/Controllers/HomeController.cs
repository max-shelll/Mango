using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.BLL.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
