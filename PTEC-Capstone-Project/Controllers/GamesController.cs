using Microsoft.AspNetCore.Mvc;
using PTEC_Capstone_Project.Services;

namespace PTEC_Capstone_Project.Controllers
{
    public class GamesController : Controller
    {
        private string _apiEndpoint = "https://api.rawg.io/api/games";
        private readonly GamesApiService _gamesApiService;

        public GamesController(GamesApiService gamesApiService)
        {
            _gamesApiService = gamesApiService;
        }

        public IActionResult Index()
        {
            // Get the game data from the API
            // Store the data in the database
            // Make a view model and pass the game data to the view
            var api_key = 
            return View();
        }
    }
}
