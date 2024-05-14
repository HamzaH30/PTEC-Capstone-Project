using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PTEC_Capstone_Project.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Navigation Properties
        public virtual ICollection<UserNotification>? Notifications { get; set; }
        public virtual ICollection<UserPost>? UserPosts { get; set; }
        public virtual ICollection<UserGame>? FavouritedGames { get; set; }
        public virtual ICollection<UserLinkedAccount>? LinkedAccounts { get; set; }
    }
}
