using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTEC_Capstone_Project.Models
{
    public class Notification
    {
        // Self Properties
        [Key] public int Id { get; set; }
        public bool IsRead { get; set; }
        public DateTime Timestamp { get; set; }

        // Foreign Key Properties
        public string SenderID { get; set; }
        public int PostID { get; set; }
        public int TypeID { get; set; }

        // Navigation Properties
        [ForeignKey("SenderID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("PostID")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Post Post { get; set; }
        [ForeignKey("TypeID")]
        public virtual NotificationType NotificationType { get; set; }
    }
}
