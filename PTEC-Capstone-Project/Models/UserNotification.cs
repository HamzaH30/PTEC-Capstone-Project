using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTEC_Capstone_Project.Models
{
    public class UserNotification
    {
        // Self Properties
        [Key] 
        public int Id { get; set; }

        // Foreign Key Properties 
        public int UserID { get; set; }
        public int NotificationID { get; set; }

        // Navigation Properties
        [ForeignKey("UserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("NotificationID")]
        public virtual Notification Notification { get; set; }
    }
}
