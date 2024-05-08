using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTEC_Capstone_Project.Models
{
    public class TournamentTeam
    {
        // Self Properties
        [Key] public int Id { get; set; }



        // Foreign Key Properties
        [ForeignKey("Tournament")] public int TournamentID { get; set; }
        [ForeignKey("Group")] public int GroupID { get; set; }
    }
}
