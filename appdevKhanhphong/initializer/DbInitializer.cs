using System;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using appdevKhanhphong.Data;
using appdevKhanhphong.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace appdevKhanhphong.initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initializer()
        {
            // check pending migrations if existed then migrate
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            // check role existed 
            if (_db.Roles.Any(r => r.Name == "Admin")) return;
            if (_db.Roles.Any(r => r.Name == "Staff")) return;
            if (_db.Roles.Any(r => r.Name == "Trainer")) return;
            if (_db.Roles.Any(r => r.Name == "Trainee")) return;

            // Create new role 
            _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();

            // Create new user
            _userManager.CreateAsync(new ApplicationUser()
            {
                UserName = "Admin@gmail.com",
                Email = "Admin@gmail.com",
                EmailConfirmed = true,
            }, "admin123@").GetAwaiter().GetResult();

            // Add user to role

            var userAdmin = _db.ApplicationUsers.First(u => u.Email == "Admin@gmail.com");
            _userManager.AddToRoleAsync(userAdmin, "Admin").GetAwaiter().GetResult();
        }
    }

}