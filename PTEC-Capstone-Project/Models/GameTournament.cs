using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTEC_Capstone_Project.Models
{
    public class GameTournament
    {
        // Self Properties
        [Key] 
        public int Id { get; set; }

        // Foreign Key Properties
         public int GameID { get; set; }
         public int TournamentID { get; set; }

        // Navigation Properties
        [ForeignKey("Game")]
        public virtual Game Game { get; set; }
        [ForeignKey("Tournament")]
        public virtual Tournament Tournament { get; set; }
    }
}
