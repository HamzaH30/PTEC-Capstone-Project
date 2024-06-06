using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PTEC_Capstone_Project.Models
{
    public class Game
    {
        // Self Properties
        [Key] 
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Title { get; set; }

        [JsonProperty("deck")]
        public string? Genre { get; set; }
        public string? Description { get; set; }
        public string? ImageFilePath {  get; set; }

        // Navigation Properties

        public virtual ICollection<ApplicationUser> e {  get; set; } 
    }
}
