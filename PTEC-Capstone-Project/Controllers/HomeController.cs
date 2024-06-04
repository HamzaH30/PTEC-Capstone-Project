using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> Index(List<GamePostViewModel> searchResults = null)
        {
            var viewModel = searchResults ?? await GetGamePostViewModelsAsync();
            viewModel = viewModel.OrderByDescending(vm => vm.TimePosted).ToList();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string search, string gameTitles, string genre, string username, string description)
        {
            IQueryable<UserPost> query = _context.UserPosts.Include(up => up.ApplicationUser).Include(up => up.Post).ThenInclude(p => p.Game);
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(up =>
                    up.Post.Game.Title.Contains(search) ||
                    (up.Post.Game.Genre != null && up.Post.Game.Genre.Contains(search)) ||
                    up.ApplicationUser.UserName.Contains(search) ||
                    up.Post.Description.Contains(search));
            }

            if (!string.IsNullOrEmpty(gameTitles))
            {
                var games = gameTitles.Split(',').Select(g => g.Trim());
                query = query.Where(up => games.Any(game => up.Post.Game.Title.Contains(game)));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(up => up.Post.Game.Genre != null && up.Post.Game.Genre.Contains(genre));
            }

            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(up => up.ApplicationUser.UserName.Contains(username));
            }

            if (!string.IsNullOrEmpty(description))
            {
                query = query.Where(up => up.Post.Description.Contains(description));
            }

            List<GamePostViewModel> viewModel = await query
                .Select(up => new GamePostViewModel
                {
                    GameName = up.Post.Game.Title,
                    UserName = up.ApplicationUser.UserName,
                    TimePosted = up.Post.Timestamp,
                    PostDescription = up.Post.Description,
                    PostID = up.PostID
                })
                .OrderByDescending(up => up.TimePosted)
                .ToListAsync();

            return View("Index", viewModel);
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
                    PostDescription = up.Post.Description,
                    PostID = up.PostID

                })
                .OrderByDescending(up => up.TimePosted)
                .ToListAsync();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /*
        [HttpGet]
        public async Task<IActionResult> CreatePost()
        {
            ViewBag.Games = new SelectList(await _context.Games.ToListAsync(), "Id", "Title");
            return View();
        }
        */

        /*
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var post = new Post
                    {
                        GameID = model.GameID,
                        Timestamp = DateTime.Now,
                        Description = model.PostDescription,
                        IsArchived = false
                    };

                    _context.Posts.Add(post);
                    await _context.SaveChangesAsync();

                    var userPost = new UserPost
                    {
                        UserID = user.Id,
                        PostID = post.Id,
                        IsCreator = true
                    };

                    _context.UserPosts.Add(userPost);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found");
                }
            }

            ViewBag.Games = new SelectList(await _context.Games.ToListAsync(), "Id", "Title");
            return View(model);
        }
        */
        [Authorize]
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
