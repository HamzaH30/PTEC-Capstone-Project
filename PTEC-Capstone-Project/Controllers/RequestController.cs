using Microsoft.AspNetCore.Mvc;
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
            

            // create request model
            Request req = new Request
            {
                Timestamp = DateTime.Now,
                PostID = postID,
                Post = post,

            };
            // sync database




            return View();
        }

        public void CreateOrFindStatus(Statuses status)
        {
            
        }
    }
}
