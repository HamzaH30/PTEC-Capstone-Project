using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTEC_Capstone_Project.Models
{
    public class UserGame
    {
        // Self Properties
        [Key] public int Id { get; set; }



        // Foreign Key Properties
        [ForeignKey("User")] public int UserID { get; set; }
        [ForeignKey("Game")] public int GameID { get; set; }
    }
}
