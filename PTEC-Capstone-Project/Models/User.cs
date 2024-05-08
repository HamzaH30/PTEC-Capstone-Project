using System.ComponentModel.DataAnnotations;

namespace PTEC_Capstone_Project.Models
{
    public class User
    {
        // Self Properties
        [Key] public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Bio {  get; set; }



        // Navigation Properties
        public virtual ICollection<UserNotification>? Notifications { get; set; }
        public virtual ICollection<UserPost>? Posts { get; set; }
        public virtual ICollection<UserGame>? FavouritedGames { get; set; }
        public virtual ICollection<UserLinkedAccount>? LinkedAccounts { get; set; }
    }
}
