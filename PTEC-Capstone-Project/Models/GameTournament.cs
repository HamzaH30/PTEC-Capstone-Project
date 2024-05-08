﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTEC_Capstone_Project.Models
{
    public class GameTournament
    {
        // Self Properties
        [Key] public int Id { get; set; }



        // Foreign Key Properties
        [ForeignKey("Game")] public int GameID { get; set; }
        [ForeignKey("Tournament")] public int TournamentID { get; set; }
    }
}
