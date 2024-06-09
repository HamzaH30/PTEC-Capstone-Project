using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PTEC_Capstone_Project.Data;
using PTEC_Capstone_Project.Models;
using System;

namespace PTEC_Capstone_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Determine the current environment (Development, Production, etc.)
            var environment = builder.Environment;

            // Retrieve the connection string from configuration settings
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException($"Connection string 'DefaultConnection' not found for {environment.EnvironmentName} environment.");

            // Configure Entity Framework with SQL Server and enable retry on failure
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                }));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Create a scope for obtaining services
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    // Apply pending migrations to the database
                    context.Database.Migrate();

                    string? seedUserPassword = builder.Configuration["SeedUserPW"];

                    // Seed the database with initial data only in the Development environment
                    if (app.Environment.IsDevelopment())
                    {
                        SeedData.Initialize(services, seedUserPassword).Wait();
                    }
                    else
                    {
                        // Regardless, we still want to seed a super admin user
                        SeedData.SeedSuperAdminUser(services, seedUserPassword).Wait();
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                    logger.LogError(ex, "An error occurred while seeding the database.");
                    throw; // Re-throw the exception to ensure the application fails to start if seeding fails
                }
            }

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                // In Development, use migrations endpoint for automatic migration application
                app.UseMigrationsEndPoint();
            }
            else
            {
                // In Production, use exception handler and enable strict transport security
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
