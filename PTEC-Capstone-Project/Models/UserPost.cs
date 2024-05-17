using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PTEC_Capstone_Project.Models
{
    public class UserPost
    {
        // Self Properties
        [Key] 
        public int Id { get; set; }
        public bool IsCreator { get; set; } = false;

        // Foreign Key Properties 
        public string UserID { get; set; }
        public int PostID { get; set; }

        // Navigation Properties
        [ForeignKey("UserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("PostID")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Post Post { get; set; }
    }
}
