using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PTEC_Capstone_Project.Models;

namespace PTEC_Capstone_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameTournament> GameTournaments { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupUser> GroupUsers { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NotificationType> NotificationTypes  { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RequestStatus> RequestStatuses { get; set; }
        public virtual DbSet<Tournament> Tournaments { get; set; }
        public virtual DbSet<TournamentTeam> TournamentTeams { get; set; }
        public virtual DbSet<UserGame> UserGames  { get; set; }
        public virtual DbSet<UserLinkedAccount> UserLinkedAccounts  { get; set; }
        public virtual DbSet<UserNotification> UserNotifications { get; set; }
        public virtual DbSet<UserPost> UserPosts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
