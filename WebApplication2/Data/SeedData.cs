using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Auth;

namespace WebApplication2.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string username, string password = "Test@1")
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var adminUser = await EnsureUser(serviceProvider, username, password);
                await EnsureRole(serviceProvider, adminUser, ApplicationUserRoles.AdminRole);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider, string username, string password)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = username,
                    Email = username,
                    EmailConfirmed = true,
                };

                var result = await userManager.CreateAsync(user, password);
            }

            if (user == null) throw new Exception("User creation failed.");

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string userId, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            IdentityResult identityResult;

            if(await roleManager.RoleExistsAsync(role) == false)
            {
                identityResult = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var user = await userManager.FindByIdAsync(userId);

            if (user == null) throw new Exception("User does not exist.");

            identityResult = await userManager.AddToRoleAsync(user, role);
            return identityResult;
        }
    }
}
