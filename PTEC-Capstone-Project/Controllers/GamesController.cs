using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PTEC_Capstone_Project.Data;
using PTEC_Capstone_Project.Models;
using PTEC_Capstone_Project.Models.ViewModels;
using PTEC_Capstone_Project.Services;

namespace PTEC_Capstone_Project.Controllers
{
    public class GamesController : Controller
    {
       
        private readonly GamesApiService _gamesApiService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GamesController(GamesApiService gamesApiService, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _gamesApiService = gamesApiService;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (!_context.Games.Any())
            {
                // If the games list is empty, make an API call to fetch games
                string _apiEndpoint = "https://www.giantbomb.com/api/games?";
                var apiResponse = await _gamesApiService.GetApiResponse(_apiEndpoint);
                var games = apiResponse?.Results ?? new List<Game>();

                // Save the fetched games to the database
                _context.Games.AddRange(games);
                await _context.SaveChangesAsync();
            }

            var user = await _userManager.GetUserAsync(User);

            var favoriteGameIds = _context.UserGames
                .Where(ug => ug.UserID == user.Id)
                .Select(ug => ug.GameID)
                .ToList();

            var allGames = _context.Games.ToList();

            var model = new GamesViewModel
            {
                Games = allGames,
                FavoriteGameIds = favoriteGameIds
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorites(string gameTitle, int? gameId)
        {
            var user = await _userManager.GetUserAsync(User);

            // Check if the game with the given title exists in the database
            Game existingGame = null;
            if (!string.IsNullOrEmpty(gameTitle))
            {
                existingGame = _context.Games.FirstOrDefault(g => g.Title == gameTitle);
            }

            // If the game with the given title is not found, try searching by gameId
            if (existingGame == null && gameId.HasValue)
            {
                existingGame = await _context.Games.FindAsync(gameId.Value);
            }

            if (existingGame == null)
            {
                // Fetch the game details from the API based on the title or ID
                if (!string.IsNullOrEmpty(gameTitle))
                {
                    var apiResponse = await _gamesApiService.SearchGames(gameTitle);
                    existingGame = apiResponse?.Results.FirstOrDefault();
                }
                else if (gameId.HasValue)
                {
                    var apiResponse = await _gamesApiService.GetGameById(gameId.Value);
                    existingGame = apiResponse?.Results;
                }

                if (existingGame != null && !string.IsNullOrEmpty(existingGame.Title))
                {
                    // Ensure the game ID is not set, so the database can generate it
                    existingGame.Id = 0;

                    // Save the fetched game to the database
                    _context.Games.Add(existingGame);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Handle the case where the game is not found in the API
                    return NotFound();
                }
            }

            // Add the game to the user's favorites
            var userGame = new UserGame
            {
                UserID = user.Id,
                GameID = existingGame.Id
            };

            _context.UserGames.Add(userGame);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromFavorites(int gameId)
        {
            var user = await _userManager.GetUserAsync(User);

            var userGame = _context.UserGames
                .FirstOrDefault(ug => ug.UserID == user.Id && ug.GameID == gameId);

            if (userGame != null)
            {
                _context.UserGames.Remove(userGame);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Search(string query)
        {
            var apiResponse = await _gamesApiService.SearchGames(query);
            var games = apiResponse?.Results ?? new List<Game>();

            var model = new GamesViewModel
            {
                Games = games,
                FavoriteGameIds = new List<int>()
            };

            return View("Index", model);
        }
    }
}
