using GetUrCourse.Services.AuthAPI.Constants;
using GetUrCourse.Services.AuthAPI.Entities;
using GetUrCourse.Services.AuthAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.AuthAPI.Infrastructure.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthDbContext _db;

        public DbInitializer(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AuthDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                return;
            }

            if (!_roleManager.RoleExistsAsync(AuthSettings.Role_Customer).GetAwaiter().GetResult())
            {
                var customerRoleResult = _roleManager.CreateAsync(new IdentityRole(AuthSettings.Role_Customer)).GetAwaiter().GetResult();

                var adminRoleResult = _roleManager.CreateAsync(new IdentityRole(AuthSettings.Role_Admin)).GetAwaiter().GetResult();

                var adminUser = new ApplicationUser
                {
                    UserName = "TestAdmin",
                    Email = "admin@admin.com",
                    Name = "Admin",
                };

                var adminResult = _userManager.CreateAsync(adminUser, "Admin123*").GetAwaiter().GetResult();

                var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@admin.com");
                if (user != null)
                {
                    var addToRoleResult = _userManager.AddToRoleAsync(user, AuthSettings.Role_Admin).GetAwaiter().GetResult();

                }
            }
        }
    }
}
