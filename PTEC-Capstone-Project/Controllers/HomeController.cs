using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PTEC_Capstone_Project.Data;
using PTEC_Capstone_Project.Models;
using System.Diagnostics;

namespace PTEC_Capstone_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        //game, user, time posted, num of players, description, rank, level requirement


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CreatePost()
        {
            return View();
        }

        public IActionResult FavouriteGames()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
