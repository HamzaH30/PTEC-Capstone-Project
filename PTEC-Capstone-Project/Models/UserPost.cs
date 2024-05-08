using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PTEC_Capstone_Project.Models
{
    public class UserPost
    {
        // Self Properties
        [Key] public int Id { get; set; }



        // Foreign Key Properties 
        [ForeignKey("User")] public int UserID { get; set; }
        [ForeignKey("Post")] public int PostID { get; set; }
    }
}
