using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTEC_Capstone_Project.Models
{
    public class Group
    {
        // Self Properties
        [Key]
        public int Id { get; set; }
        public string? GroupName { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }

        // Foreign Key Properties
        public string CreatorID { get; set; }
        public int GameID { get; set; }

        // Navigation Properties 
        public virtual ICollection<GroupUser> GroupUsers { get; set; }
        [ForeignKey("CreatorID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("GameID")]
        public virtual Game Game { get; set; }
    }
}
