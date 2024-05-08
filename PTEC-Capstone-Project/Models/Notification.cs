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
        [ForeignKey("User")] public int RecieverID { get; set; }
        [ForeignKey("Post")] public int PostID { get; set; }
        [ForeignKey("NotificationType")] public int TypeID { get; set; }



        // Navigation Properties

    }
}
