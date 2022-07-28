using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using restaurant.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            try { 
            var scope = host.Services.CreateScope();

            var ctx = scope.ServiceProvider.GetRequiredService<DBRContext>();
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            ctx.Database.EnsureCreated();

            var adminRole = new IdentityRole("Admin");
            if (!ctx.Roles.Any())
            {
                //creates a role
                roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
            }
            if (!ctx.Users.Any(u => u.UserName == "admin"))
            {
                //creates an admin
                var adminUser = new IdentityUser
                {
                    UserName = "admin",
                    Email = "restaurant@admin.com"
                };
                var result = userMgr.CreateAsync(adminUser, "password").GetAwaiter().GetResult(); //by debugging: must be "succeeded" 

                userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();

            }
        }
        catch(Exception e)
        {
                Console.WriteLine(e);
        }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
