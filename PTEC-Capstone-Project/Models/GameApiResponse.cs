using Newtonsoft.Json;

namespace PTEC_Capstone_Project.Models
{
    public class GameApiResponse
    {
        [JsonProperty("results")]
        public Game Results { get; set; }
    }
}
