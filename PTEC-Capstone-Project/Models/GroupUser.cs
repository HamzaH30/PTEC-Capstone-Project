using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTEC_Capstone_Project.Models
{
    public class GroupUser
    {
        // Self Properties
        [Key] 
        public int Id { get; set; }

        // Foreign Key Properties
        public string UserID { get; set; }
        public int GroupID { get; set; }

        // Naviation Properties
        [ForeignKey("UserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("GroupID")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Group Group { get; set; }
    }
}
