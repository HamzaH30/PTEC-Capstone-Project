using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PTEC_Capstone_Project.Data;
using PTEC_Capstone_Project.Models.ViewModels;
using PTEC_Capstone_Project.Models;
using Microsoft.AspNetCore.Authorization;

namespace PTEC_Capstone_Project.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SuperAdminController(ApplicationDbContext context, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            _context = context;
            _userManager = um;
            _roleManager = rm;
        }

        [HttpGet] 
        public async Task<IActionResult> ManageUserRoles()
        {
            ReviewUsersViewModel vm = new()
            {
                Roles = _roleManager.Roles.Select(r => r.Name).ToList()!
            };

            foreach (var user in _userManager.Users)
            {
                vm.Members.Add(
                    new ReviewUsersViewModel.Member()
                    {
                        Name = user.UserName ?? "no user name",
                        Roles = (await _userManager.GetRolesAsync(user)).ToHashSet()
                    }
                    );
            }

            return View(vm);
        }



        public IActionResult Index;

    }
}
