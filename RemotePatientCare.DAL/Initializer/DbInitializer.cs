using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RemotePatientCare.DAL.Data;
using RemotePatientCare.DAL.Identity;
using RemotePatientCare.DAL.Models;
using RemotePatientCare.Utility;

namespace RemotePatientCare.DAL.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly RemotePatientCareDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public DbInitializer(RemotePatientCareDbContext db, UserManager<ApplicationUser> userManager,
                             RoleManager<ApplicationRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task Initialize()
        {
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }
            if (!_roleManager.RoleExistsAsync(CustomRoles.GlobalAdmin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new ApplicationRole { Name = CustomRoles.User });
                await _roleManager.CreateAsync(new ApplicationRole { Name = CustomRoles.GlobalAdmin });
            }
            else
            {
                return;
            }

            await _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                PhoneNumber = "1234567890",               
            }, "Admin123*");

            await _db.BaseUsers.AddAsync(new User
            {
                Id = _db.Users.FirstOrDefault(u => u.Email == "admin@gmail.com")!.Id,
                FirstName = "Admin",
                LastName = "Global",
                Patronymic = "Test",
                Phone = "00000000",
                Email = "admin@gmail.com",
                BirthDate = DateTime.Now,
            });

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == "admin@gmail.com")!;
            await _userManager.AddToRolesAsync(user!, new List<string>()
            {
                    CustomRoles.User,
                    CustomRoles.GlobalAdmin
            });

            await _db.SaveChangesAsync();
        }
    }
}
