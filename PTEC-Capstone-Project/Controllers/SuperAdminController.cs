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

        [HttpPost]
        public async Task<IActionResult> ToggleRole(string roleName, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound($"User '{userName}' not found.");
            }


            if (User.IsInRole(Constants.AdminRole) && !User.IsInRole(Constants.SuperAdminRole) && (roleName == Constants.AdminRole || roleName == Constants.SuperAdminRole || (await _userManager.IsInRoleAsync(user, Constants.AdminRole) || await _userManager.IsInRoleAsync(user, Constants.SuperAdminRole))))
            {
                return Forbid();
            }

            var userIsInRole = await _userManager.IsInRoleAsync(user, roleName);
            if (userIsInRole)
            {
                if (roleName == Constants.SuperAdminRole && User.Identity.Name == userName)
                {
                    // Super admin is trying to remove their own super admin role
                    ViewBag.WarningMessage = "You are about to remove your super admin role. This action cannot be undone.";
                    return View("ConfirmSuperAdminRoleRemoval");
                }
                // If the user is already in the role, remove the role
                await _userManager.RemoveFromRoleAsync(user, roleName);
            }
            else
            {
                // If the user is not in the role, add the role
                await _userManager.AddToRoleAsync(user, roleName);
            }
            return RedirectToAction("ManageUserRoles");
        }


        public IActionResult Index;

    }
}
