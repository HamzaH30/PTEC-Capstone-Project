using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PTEC_Capstone_Project.Data;
using PTEC_Capstone_Project.Models;

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

            List<Request> requests = [];
            List<UserRequests> userReqs = _context.UserRequests.Where(ur => ur.UserID == user.Id).ToList();
            
            foreach(UserRequests ur in  userReqs)
            {
                requests.Add(ur.Request);
            }

            return View(requests);
        }

        public async Task<IActionResult> Notifications()
        {
            ApplicationUser? user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            return View();
        }
    }
}
