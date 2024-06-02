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

        [HttpPost]
        public void CreateRequest(int postID)
        {
            Console.WriteLine($"Button works you cuck {postID}");
        }
    }
}
