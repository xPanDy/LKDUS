using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.Data
{
    public static class SeedData
    {
        public async static Task Seed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);

        }
        private async static Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            /*   if (await userManager.FindByEmailAsync("admin@finieris.lv") == null)
               {
                   var user = new IdentityUser
                   {
                       UserName = "master",

                       Email = "master@finieris.lv"
                   };

                   var result = await userManager.CreateAsync(user, "master");
                   if (result.Succeeded)
                   {
                       await userManager.AddToRoleAsync(user, "Master");
                   }

               }*/
               if (await userManager.FindByEmailAsync("admin@finieris.lv") == null)
               {
                   var user = new IdentityUser
                   {
                       UserName = "admin",

                       Email = "admin@finieris.lv"
                   };

                  var result =  await userManager.CreateAsync(user, "1111");
                   if (result.Succeeded)
                   {
                       await userManager.AddToRoleAsync(user, "Admin");
                   }

               }
               if (await userManager.FindByEmailAsync("operator@finieris.lv") == null)
               {
                   var user = new IdentityUser
                   {
                       UserName = "operator",
                       Email = "operator@finieris.lv"
                   };

                   var result = await userManager.CreateAsync(user, "1111");
                   if (result.Succeeded)
                   {
                       await userManager.AddToRoleAsync(user, "Operator");
                   }

               }
              
           
            

            if (await userManager.FindByEmailAsync("kristaps@finieris.lv") == null)
            {
                 
                var user = new IdentityUser
                {
                    UserName = "kristaps",
                     
                    Email = "kristaps@finieris.lv"
                };

                var result = await userManager.CreateAsync(user, "1111" );
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Master");
                }
                

            }
        }
        private async static Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Master")  )
            {
                var role = new IdentityRole
                {
                    Name = "Master"
                };
               await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("Operator"))
            {
                var role = new IdentityRole
                {
                    Name = "Operator"
                };
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                await roleManager.CreateAsync(role);
            }
        }
    }
}
