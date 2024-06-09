using Newtonsoft.Json;
using PTEC_Capstone_Project.Models;


namespace PTEC_Capstone_Project.Services
{
    public class GamesApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GamesApiService(HttpClient client, string apiKey)
        {
            _httpClient = client;
            _apiKey = apiKey;

            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GameApp/1.0; +https://ptec-capstone-project.azurewebsites.net/)");
        }

        public async Task<GamesApiResponse> GetApiResponse(string url)
        {
            string requestUrl = $"{url}api_key={_apiKey}&format=json";
            // Send a GET request to the API
            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

            // Check if the response is successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                string content = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON content
                GamesApiResponse apiResponse = JsonConvert.DeserializeObject<GamesApiResponse>(content);
                return apiResponse;

            }
            return null;
        }

        public async Task<GameApiResponse> GetGameById(int gameId)
        {
            string requestUrl = $"https://www.giantbomb.com/api/game/{gameId}/?api_key={_apiKey}&format=json";

            // Send a GET request to the API
            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

            // Check if the response is successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                string content = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON content into a collection of games
                GameApiResponse apiResponse = JsonConvert.DeserializeObject<GameApiResponse>(content);

                return apiResponse;
            }
            else
            {
                // Handle the case where the API request fails
                // For example, you can throw an exception or return null
                return null;
            }
        }

        public async Task<GamesApiResponse> SearchGames(string query)
        {
            string searchEndpoint = $"https://www.giantbomb.com/api/search/?query={query}&resources=game&api_key={_apiKey}&format=json";
            HttpResponseMessage response = await _httpClient.GetAsync(searchEndpoint);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                GamesApiResponse apiResponse = JsonConvert.DeserializeObject<GamesApiResponse>(content);
                return apiResponse;
            }
            return null;
        }
    }
}
