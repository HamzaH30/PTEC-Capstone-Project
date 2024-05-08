using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTEC_Capstone_Project.Models
{
    public class Request
    {
        // Self Properties
        [Key] public int Id { get; set; }
        public DateTime Timestamp { get; set; }



        // Foreign Key Property
        [ForeignKey("User")] public int SenderID { get; set; }
        [ForeignKey("Group")] public int GroupID { get; set; }
        [ForeignKey("RequestStatus")] public int StatusID { get; set; }

        
    }
}
