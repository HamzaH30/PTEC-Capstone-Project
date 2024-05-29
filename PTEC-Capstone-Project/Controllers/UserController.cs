using Microsoft.AspNetCore.Mvc;

namespace PTEC_Capstone_Project.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
