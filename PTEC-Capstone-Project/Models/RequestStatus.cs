using System.ComponentModel.DataAnnotations;

namespace PTEC_Capstone_Project.Models
{
    public class RequestStatus
    {
        // Self Properties
        [Key] public int Id { get; set; }
        public string Description { get; set; }
    }
}
