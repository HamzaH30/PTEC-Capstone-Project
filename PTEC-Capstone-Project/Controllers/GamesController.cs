using Microsoft.AspNetCore.Mvc;

namespace PTEC_Capstone_Project.Controllers
{
    public class GamesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
