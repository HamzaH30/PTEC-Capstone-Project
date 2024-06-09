using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTEC_Capstone_Project.Data;
using PTEC_Capstone_Project.Models;
using PTEC_Capstone_Project.Models.ViewModels;

namespace PTEC_Capstone_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Requests()
        {
            ApplicationUser? user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            List<UserRequests> userReqs = _context.UserRequests.Where(ur => ur.UserID == user.Id).Include(ur => ur.Request).Include(ur => ur.Request.RequestStatus).ToList();
            List<SeeRequestsViewModel> reqVM = new List<SeeRequestsViewModel>();


            foreach (UserRequests ur in userReqs)
            {
                List<UserPost> posts = _context.UserPosts.Where(up => up.PostID == ur.Request.PostID).Include(up => up.ApplicationUser).ToList();
                foreach (UserPost up in posts)
                {
                    SeeRequestsViewModel req = new SeeRequestsViewModel
                    {
                        UserName = up.ApplicationUser.UserName,
                        Status = ur.Request.RequestStatus.Name.ToString()
                    };

                    

                    reqVM.Add(req);
                }
                
            }
            
            return View(reqVM);
        }

        public async Task<IActionResult> Notifications()
        {
            ApplicationUser? user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            List<UserNotification> userNotifs = _context.UserNotifications.Where(ur => ur.UserID == user.Id).Include(ur => ur.Notification).Include(ur => ur.Notification.NotificationType).ToList();
            List<SeeNotifViewModel> notifVM = new List<SeeNotifViewModel>();

            foreach (UserNotification un in userNotifs)
            {
                List<UserRequests> reqs = _context.UserRequests.Where(ur => ur.Request.PostID == un.Notification.PostID).Include(ur => ur.ApplicationUser).ToList();
                foreach (UserRequests ur in reqs)
                {
                    SeeNotifViewModel notif = new SeeNotifViewModel
                    {
                        postID = ur.Request.PostID,
                        recieverID = un.UserID,
                        senderID = ur.ApplicationUser.Id,
                        userName = ur.ApplicationUser.UserName,
                        type = un.Notification.NotificationType.Name.ToString(),
                        timstamp = un.Notification.Timestamp,
                        isRead = false,
                    };



                    notifVM.Add(notif);
                }
            }

            return View(notifVM);
        }
    }
}
