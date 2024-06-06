using Microsoft.AspNetCore.Mvc;
using PTEC_Capstone_Project.Models;
using PTEC_Capstone_Project.Services;

namespace PTEC_Capstone_Project.Controllers
{
    public class GamesController : Controller
    {
       
        private readonly GamesApiService _gamesApiService;

        public GamesController(GamesApiService gamesApiService)
        {
            _gamesApiService = gamesApiService;
        }

        public async Task<IActionResult> Index()
        {
            string _apiEndpoint = "https://www.giantbomb.com/api/games?";
            var apiResponse = await _gamesApiService.GetApiResponse(_apiEndpoint);
            var games = apiResponse?.Results ?? new List<Game>();
            return View(games);
        }
    }
}
