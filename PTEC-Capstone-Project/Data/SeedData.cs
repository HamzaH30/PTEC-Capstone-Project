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

            await SeedSuperAdminUser(serviceProvider, seedUserPw);
        }
        
        /// <summary>
        /// A specific method meant to create a super admin role
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="seedUserPw"></param>
        /// <returns></returns>
        public static async Task SeedSuperAdminUser(IServiceProvider serviceProvider, string? seedUserPw)
        {
            string superAdminUserName = "SuperAdmin";
            string adminId = await SeedUser(serviceProvider, superAdminUserName, seedUserPw);

            await SeedUserRole(serviceProvider, adminId, Constants.SuperAdminRole);
        }

        /// <summary>
        /// Method to create a seed user.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<string> SeedUser(IServiceProvider serviceProvider, string userName, string password)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>()!;

            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = userName,
                };

                var result = await userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create seed user");
                }
            }
            return user.Id;
        }

        /// <summary>
        /// This is a method that creates a role, if it does not exist. Then will assign a user to that role.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<IdentityResult> SeedUserRole(IServiceProvider serviceProvider, string userId, string roleName)
        {
            // Check if role exists, if not, create it
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>()!;

            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // Assign user to this role
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>()!;
            var user = await userManager.FindByIdAsync(userId.ToString()) ?? throw new Exception("Seed user not found");
            IdentityResult result = await userManager.AddToRoleAsync(user, roleName);
            
            return result;
        }
    }
}
