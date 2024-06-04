using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using PTEC_Capstone_Project.Data;
using PTEC_Capstone_Project.Models;

namespace PTEC_Capstone_Project.Controllers
{
    public class RequestController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RequestController(ApplicationDbContext context) 
        { 
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateRequest(int postID)
        {
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

            return View();
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
