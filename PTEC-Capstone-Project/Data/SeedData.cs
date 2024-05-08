using PTEC_Capstone_Project.Models;
using Microsoft.AspNetCore.Identity;

namespace PTEC_Capstone_Project.Data
{
    public class SeedData
    {

        public static async Task Initialize(IServiceProvider serviceProvider, string? seedUserPw)
        {
            if (seedUserPw == null)
            {
                throw new Exception("No seed password!");
            }

            string adminId = await SeedUser(serviceProvider, "seedUser@superAdmin.com", seedUserPw);

            await SeedRole(serviceProvider, adminId, Constants.SuperAdminRole);
        }

        public static async Task<string> SeedUser(IServiceProvider serviceProvider, string userName, string password)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>()!;

            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = new ApplicationUser()
                {
                    Username = userName,
                    Email = "seedUser@superadmin.com",
                    Bio = "Bio Here"
                };
                var result = await userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create seed user");
                }
            }
            return user.Id;
        }

        public static async Task<IdentityResult> SeedRole(IServiceProvider serviceProvider, string userId, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>()!;

            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>()!;

            var user = await userManager.FindByIdAsync(userId) ?? throw new Exception("Seed user not found");

            IdentityResult result = await userManager.AddToRoleAsync(user, role);

            return result;
        }

    }
}
