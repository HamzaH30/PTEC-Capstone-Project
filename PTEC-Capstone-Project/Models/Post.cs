using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTEC_Capstone_Project.Models
{
    public class Post
    {
        // Self Properties
        [Key]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public bool IsArchived { get; set; } = false;

        // Foreign Key Properties
        public string UserID { get; set; }
        public int StatusID { get; set; }
        public int GroupID { get; set; }
        public int GameID { get; set; }

        // Navigation Properties
        [ForeignKey("UserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("GroupID")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Group Group { get; set; }

        [ForeignKey("GameID")]
        public virtual Game Game { get; set; }
    }
}
