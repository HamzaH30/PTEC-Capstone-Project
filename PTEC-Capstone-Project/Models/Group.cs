using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTEC_Capstone_Project.Models
{
    public class Group
    {
        // Self Properties
        [Key] public int Id { get; set; }
        public string GroupName { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }



        // Foreign Key Properties
        [ForeignKey("ApplicationUser")] public int CreatorID { get; set; }
        [ForeignKey("Game")] public int GameID { get; set; }
        [ForeignKey("GroupStatus")] public int StatusID { get; set; }



        // Navigation Properties 
        public virtual ICollection<GroupUser> GroupUsers { get; set; }
    }
}
