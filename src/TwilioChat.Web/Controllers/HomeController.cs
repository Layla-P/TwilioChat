using Microsoft.AspNetCore.Mvc;

namespace TwilioChat.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
