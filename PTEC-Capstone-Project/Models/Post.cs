using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTEC_Capstone_Project.Models
{
    public class Post
    {
        // Self Properties
        [Key] public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }



        // Foreign Key Properties
        [ForeignKey("ApplicationUser")] public int UserID { get; set; }
        [ForeignKey("PostStatus")] public int StatusID { get; set; }
        [ForeignKey("Group")] public int GroupID { get; set; }
        [ForeignKey("Game")] public int GameID { get; set; }
    }
}
