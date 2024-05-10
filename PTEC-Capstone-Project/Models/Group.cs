using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTEC_Capstone_Project.Models
{
    public class Group
    {
        // Self Properties
        [Key]
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }


        // Foreign Key Properties
        public int CreatorID { get; set; }
        public int GameID { get; set; }
        public int StatusID { get; set; }

        // Navigation Properties 
        public virtual ICollection<GroupUser> GroupUsers { get; set; }
        [ForeignKey("ApplicationUser")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("Game")]
        public virtual Game Game { get; set; }
        [ForeignKey("GroupStatus")]
        public virtual GroupStatus GroupStatus { get; set; }
    }
}
