using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTEC_Capstone_Project.Models
{
    public class TournamentTeam
    {
        // Self Properties
        [Key] 
        public int Id { get; set; }

        // Foreign Key Properties
        public int TournamentID { get; set; }
        public int GroupID { get; set; }

        // Navigation properties
        [ForeignKey("TournamentID")]
        public virtual Tournament Tournament { get; set; }
        [ForeignKey("GroupID")]
        public virtual Group Group { get; set; }
    }
}
