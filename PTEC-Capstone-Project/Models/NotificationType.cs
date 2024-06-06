using System.ComponentModel.DataAnnotations;

namespace PTEC_Capstone_Project.Models
{
    public class NotificationType
    {
        // Self Properties
        [Key] 
        public int Id { get; set; }
        public Types Name { get; set; }
    }

    public enum Types
    {
        Pending = 0,
        Accepted = 1,
        Denied = 2
    }
}
