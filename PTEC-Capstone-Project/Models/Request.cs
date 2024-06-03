using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTEC_Capstone_Project.Models
{
    public class Request
    {
        // Self Properties
        [Key] 
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }



        // Foreign Key Property
        public string RecieverID { get; set; }
        public int StatusID { get; set; }



        // Navigation Properties
        [ForeignKey("RecieverID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("StatusID")]
        public virtual RequestStatus RequestStatus { get; set; }
    }
}
