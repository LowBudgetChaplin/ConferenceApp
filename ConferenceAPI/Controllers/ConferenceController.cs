using Microsoft.AspNetCore.Mvc;

namespace ConferenceAPI.Controllers
{
    public class ConferenceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
