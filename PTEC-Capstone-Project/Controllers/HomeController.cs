using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTEC_Capstone_Project.Data;
using PTEC_Capstone_Project.Models;
using PTEC_Capstone_Project.Models.ViewModels;
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

        public async Task<IActionResult> Index()
        {
            var viewModel = await GetGamePostViewModelsAsync();
            return View(viewModel);
        }

        private async Task<List<GamePostViewModel>> GetGamePostViewModelsAsync()
        {
            return await _context.UserPosts
                .Include(up => up.ApplicationUser)
                .Include(up => up.Post)
                    .ThenInclude(p => p.Game)
                .Select(up => new GamePostViewModel
                {
                    GameName = up.Post.Game.Title,
                    UserName = up.ApplicationUser.UserName,
                    TimePosted = up.Post.Timestamp,
                    PostDescription = up.Post.Description
                })
                .ToListAsync();
        }

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
