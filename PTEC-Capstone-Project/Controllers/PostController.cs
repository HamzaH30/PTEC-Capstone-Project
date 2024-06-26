﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PTEC_Capstone_Project.Data;
using PTEC_Capstone_Project.Models.ViewModels;
using PTEC_Capstone_Project.Models;
using Microsoft.AspNetCore.Authorization;

namespace PTEC_Capstone_Project.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User); 

            var favoriteGameIds = _context.UserGames
                .Where(ug => ug.UserID == user.Id)
                .Select(ug => ug.GameID)
                .ToList();

            var favoriteGames = await _context.Games
                .Where(g => favoriteGameIds.Contains(g.Id))
                .ToListAsync();

            ViewBag.Games = new SelectList(favoriteGames, "Id", "Title");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var post = new Post
                    {
                        GameID = model.GameID,
                        Timestamp = DateTime.Now,
                        Description = model.PostDescription,
                        IsArchived = false
                    };

                    _context.Posts.Add(post);
                    await _context.SaveChangesAsync();

                    var userPost = new UserPost
                    {
                        UserID = user.Id,
                        PostID = post.Id,
                        IsCreator = true
                    };

                    _context.UserPosts.Add(userPost);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found");
                }
            }

            ViewBag.Games = new SelectList(await _context.Games.ToListAsync(), "Id", "Title");
            return View("Index", model);
        }

        public IActionResult PostRequests()
        {
            return View();
        }
    }
}
