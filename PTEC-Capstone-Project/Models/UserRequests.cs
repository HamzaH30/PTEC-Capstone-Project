using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PTEC_Capstone_Project.Models
{
    public class UserRequests
    {
        // Self Properties
        [Key]
        public int Id { get; set; }

        // Foreign Key Properties 
        public string UserID { get; set; }
        public int RequestID { get; set; }
        public int PostID { get; set; }

        // Navigation Properties
        [ForeignKey("UserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("RequestID")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Request Request { get; set; }
    }
}
