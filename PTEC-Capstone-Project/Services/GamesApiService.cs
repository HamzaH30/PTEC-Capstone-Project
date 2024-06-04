using Newtonsoft.Json;

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
        }

        public async Task GetApiResponse(string url)
        {
            // Send a GET request to the API
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            // Check if the response is successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                string content = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON content
                

            }
        }
    }
}
