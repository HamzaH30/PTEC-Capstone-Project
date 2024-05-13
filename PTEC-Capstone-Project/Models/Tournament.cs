using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTEC_Capstone_Project.Models
{
    public class Tournament
    {
        // Self Properties
        [Key] 
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        // Foreign Key Properties
        public int GameID { get; set; }

        // Navigation Properties
        public virtual ICollection<TournamentTeam>? Teams { get; set; }
        [ForeignKey("GameID")]
        public virtual Game Game { get; set; }
    }
}
