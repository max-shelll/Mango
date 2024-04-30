using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.BLL.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View();
		}
	}
}
