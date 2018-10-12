using Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.API.Seeding
{
    public class IdentityInitializer
    {
        private RoleManager<IdentityRole> roleManager;
        private ReCircleDbContext context;
        private UserManager<User> userManager;

        public IdentityInitializer(UserManager<User> userMgr, RoleManager<IdentityRole> roleMgr, ReCircleDbContext context)
        {
            userManager = userMgr;
            roleManager = roleMgr;
            this.context = context;
        }

        public async Task Seed()
        {
            var user = await userManager.FindByNameAsync("devtester");

            if (user != null)
            {

                user = new User()
                {
                    UserName = "devtesting",
                    Email = "dev@dev.com"
                };

                var userResult = await userManager.CreateAsync(user, "Password!123");
                if (userResult.Succeeded)
                {
                    user = context.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();
                    var roleResult = await userManager.AddToRoleAsync(user, "Admin");
                }
               
                var claimResult = await userManager.AddClaimAsync(user, new Claim("SuperUser", "True"));

                if (!userResult.Succeeded || !claimResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }
            }

            if (!(await roleManager.RoleExistsAsync("Admin")))
            {
                var role = new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };

                await roleManager.CreateAsync(role);

            }
            if (!(await roleManager.RoleExistsAsync("Student")))
            {
                var role = new IdentityRole()
                {
                    Name = "Student",
                    NormalizedName = "STUDENT"
                };
                var result = await roleManager.CreateAsync(role);
                ;
            }
            if (!(await roleManager.RoleExistsAsync("Teacher")))
            {
                var role = new IdentityRole()
                {
                    Name = "Teacher",
                    NormalizedName = "TEACHER"
                };
                await roleManager.CreateAsync(role);
            }
            if (!(await roleManager.RoleExistsAsync("Librarian")))
            {
                var role = new IdentityRole()
                {
                    Name = "Librarian",
                    NormalizedName = "LIBRARIAN"
                };
                await roleManager.CreateAsync(role);

            }
            
        }

    }


}
