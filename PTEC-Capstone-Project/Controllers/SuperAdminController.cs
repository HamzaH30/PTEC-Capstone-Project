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
    [Authorize(Roles = Constants.SuperAdminRole)]
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
        public async Task<IActionResult> Index()
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
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmRoleRemoval(string roleName, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound($"User '{userName}' not found.");
            }

            var userIsInRole = await _userManager.IsInRoleAsync(user, roleName);
            await _userManager.RemoveFromRoleAsync(user, roleName);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Search(string search, string roles, string username)
        {
            IQueryable<ApplicationUser> query = _userManager.Users;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u =>
                    u.UserName.Contains(search) ||
                    u.Email.Contains(search));
            }

            if (!string.IsNullOrEmpty(roles))
            {
                var roleList = roles.Split(',').Select(r => r.Trim()).ToList();
                var usersInRoles = new List<ApplicationUser>();

                foreach (var role in roleList)
                {
                    var usersInRole = await _userManager.GetUsersInRoleAsync(role);
                    usersInRoles.AddRange(usersInRole);
                }

                // Get distinct users in roles to avoid duplicates
                var distinctUsersInRoles = usersInRoles.Distinct().Select(u => u.Id).ToList();

                // Filter the query to include only users present in distinctUsersInRoles
                query = query.Where(u => distinctUsersInRoles.Contains(u.Id));
            }

            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(u => u.UserName.Contains(username));
            }

            var usersList = await query.ToListAsync();

            List<ReviewUsersViewModel.Member> members = usersList
                .Select(u => new ReviewUsersViewModel.Member
                {
                    Name = u.UserName,
                    Roles = _userManager.GetRolesAsync(u).Result.ToHashSet()
                })
                .OrderBy(u => u.Name)
                .ToList();


            ReviewUsersViewModel vm = new()
            {
                Roles = _roleManager.Roles.Select(r => r.Name).ToList(),
                Members = members
            };

            return View("Index", vm);
        }

        /*
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            // make a list or group of users with each users id, username, and roles
            var users = _userManager.Users.ToList();
            var userRoles = new Dictionary<string, List<string>>();

            foreach (var user in users)
            {
                var roles = _userManager.GetRolesAsync((ApplicationUser)user).Result.ToList();
                userRoles.Add(user.Id, roles);
            }
            ViewBag.UserRoles = userRoles;


            return View(users);

        }
        */


    }
}
