using Newtonsoft.Json;

namespace PTEC_Capstone_Project.Models
{
    public class GamesApiResponse
    {
        [JsonProperty("results")]
        public List<Game> Results { get; set; }
    }
}
