using AlphaS_Web.Areas.Identity.Data;
using AlphaS_Web.Contexts;
using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Utils
{
    public class IdentityInitializer
    {
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<MongoRole> roleManager, CounterContext counterContext)
        {
            string adminEmail = "adminMail@mail.ru";
            string adminName = "adminUser";
            string password = "zxczxc";


            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new MongoRole("admin"));
            }
            if (await userManager.FindByNameAsync(adminName) == null)
            {
                int new_id = counterContext.GetNextId("user");
                ApplicationUser admin = new ApplicationUser { UserId = new_id, Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
