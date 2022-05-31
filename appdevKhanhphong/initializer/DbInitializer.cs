using System;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using appdevKhanhphong.Data;
using appdevKhanhphong.Models;
using appdevKhanhphong.Utility;
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
            _roleManager.CreateAsync(new IdentityRole("Staff")).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole("Trainer")).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole("Trainee")).GetAwaiter().GetResult();

            // Create new user
            _userManager.CreateAsync(new ApplicationUser()
            {
                UserName = "Admin@gmail.com",
                Email = "Admin@gmail.com",
                EmailConfirmed = true,
            }, "Admin123@").GetAwaiter().GetResult();

            // Add user to role admin

            var userAdmin = _db.ApplicationUsers.First(u => u.Email == "Admin@gmail.com");
            _userManager.AddToRoleAsync(userAdmin, "Admin").GetAwaiter().GetResult();
            
            //create new staff user 
            _userManager.CreateAsync(new ApplicationUser()
            {
                UserName = "staff@gmail.com",
                Email = "staff@gmail.com",
                EmailConfirmed = true,
                Name = "Staff"
            }, "Staff123@").GetAwaiter().GetResult();
            
            //Add user role staff
            var userStaff = _db.ApplicationUsers.First(u => u.Email == "Staff@gmail.com");
            _userManager.AddToRoleAsync(userStaff, "Staff").GetAwaiter().GetResult();
             
            // Create new trainer user 
            _userManager.CreateAsync(new Trainer()
            {
                UserName = "Trainer@gmail.com",
                Email = "Trainer@gmail.com",
                EmailConfirmed = true,
                Name = "Trainer",
                Type = TypeOfTrainer.Internal,
            },"Trainer123@").GetAwaiter().GetResult();
            
            // Create new trainee user
            _userManager.CreateAsync(new Trainee()
            {
                UserName = "Trainee@gmail.com",
                Email = "Trainee@gmail.com",
                EmailConfirmed = true,
                Name = "Trainee",
               Age = 18,
               DateOfBirth = DateTime.Now,
               Education = "Hight School",
               MainProgramingLanguage = "C#",
               ToeicScore = 800,
               Experience = "Fresher",
               Department = Department.Development,
               Location = "DN"
            },"Trainee123@").GetAwaiter().GetResult();
            
            // Add user to role Trainee
            var usserTrainee = _db.ApplicationUsers.First(u => u.Email == "Trainee123@gmail.com");
            _userManager.AddToRoleAsync(usserTrainee, "Trainee").GetAwaiter().GetResult();

        }
    }

}