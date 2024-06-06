using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using PTEC_Capstone_Project.Data;
using PTEC_Capstone_Project.Models;

namespace PTEC_Capstone_Project.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public RequestController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) 
        { 
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateRequest(int postID)
        {
            // get current user 
            ApplicationUser user = await _userManager.GetUserAsync(User);

            // check to ensure user isnt null
            if (user == null) 
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // check to make sure request by current user for this post doesnt exist
            UserRequests checkedUserReq = _context.UserRequests
                                        .Where(ur => ur.UserID == user.Id && ur.Request.PostID == postID)
                                        .FirstOrDefault();

            if (checkedUserReq != null)
            {
                return View("CannotRequest");
            }

            // find post
            Post post = _context.Posts.Where(p => p.Id == postID).FirstOrDefault()!;

            // create/find status model
            RequestStatus reqSts = CreateOrFindStatus(Statuses.Pending);

            // create request model
            Request req = new Request
            {
                Timestamp = DateTime.Now,
                PostID = postID,
                Post = post,
                StatusID = reqSts.Id,
                RequestStatus = reqSts
            };

            // sync database
            _context.Requests.Add(req);
            _context.SaveChanges();


            // create userRequest model 
            UserRequests userReq = new UserRequests
            {
                RequestID = req.Id,
                Request = req,
                UserID = user.Id,
                ApplicationUser = user,
            };

            _context.UserRequests.Add(userReq);
            _context.SaveChanges();

            return View("RequestSuccess");
        }

        public RequestStatus CreateOrFindStatus(Statuses status)
        {
            // does the status exist
            RequestStatus? reqSts = _context.RequestStatuses.Where(rs => rs.Name == status).FirstOrDefault();

            // if not create status\
            if (reqSts == null) 
            {
                reqSts = new RequestStatus
                {
                    Name = status
                };

                _context.RequestStatuses.Add(reqSts);
                _context.SaveChanges();
            }

            // if it does return 
            return reqSts;
        }
    }
}
