using Microsoft.AspNetCore.Mvc;

namespace PTEC_Capstone_Project.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Requests()
        {
            return View();
        }

        public IActionResult Notifications()
        {
            return View();
        }
    }
}
