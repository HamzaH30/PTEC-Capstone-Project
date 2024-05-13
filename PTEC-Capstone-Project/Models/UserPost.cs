using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PTEC_Capstone_Project.Models
{
    public class UserPost
    {
        // Self Properties
        [Key] 
        public int Id { get; set; }

        // Foreign Key Properties 
        public int UserID { get; set; }
        public int PostID { get; set; }

        // Navigation Properties
        [ForeignKey("UserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("PostID")]
        public virtual Post Post { get; set; }
    }
}
