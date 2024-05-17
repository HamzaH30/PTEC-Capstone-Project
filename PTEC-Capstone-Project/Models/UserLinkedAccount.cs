using System.ComponentModel.DataAnnotations;

namespace PTEC_Capstone_Project.Models
{
    public class UserLinkedAccount
    {
        // Self Properties
        [Key] 
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string ServiceName { get; set; }
    }
}
