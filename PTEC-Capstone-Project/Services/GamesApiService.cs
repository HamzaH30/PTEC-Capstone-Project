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

            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/125.0.0.0 Safari/537.36 Edg/125.0.0.0");
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
    }
}
