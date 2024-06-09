using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTEC_Capstone_Project.Models
{
    public class UserGame
    {
        // Self Properties
        [Key] 
        public int Id { get; set; }

        // Foreign Key Properties
        [Required]
        public string UserID { get; set; }
        [Required]
        public int GameID { get; set; }

        // Navigation properties
        [ForeignKey("UserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("GameID")]
        public virtual Game Game { get; set; }
    }
}
