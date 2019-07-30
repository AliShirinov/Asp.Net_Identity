using HRApp.Data;
using HRApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApp.Core
{
    public static class Seed
    {
        public static async Task InvokeAsync(IServiceScope scope, HRDbContext db)
        {
            
            if (!db.Warehouses.Any())
            {
                await db.Warehouses.AddRangeAsync(new Warehouse
                {
                    Name = "NewYorker",
                    Address = "."
                }, new Warehouse
                {
                    Name="Celio",
                    Address="."
                },new Warehouse
                {
                    Name="Emporium",
                    Address="."
                },new Warehouse
                {
                    Name="Mango",
                    Address="."
                }
                 );

                await db.SaveChangesAsync();
                
            }

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (!roleManager.Roles.Any())
            {
                string[] roles = { "Admin", "HR", "PayrollSpecialist", "DepartmentHead" };
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role
                    });
                }
            }



            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            
            if (!db.Users.Any())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                User admin = new User
                {
                    Name = "Admin",
                    Surname="Admin",
                    UserName="Admin12",
                    Email = "admin@gmail.com",

                };
                var adminCreateResult = await userManager.CreateAsync(admin, configuration["Admin:Password"]);

                if (adminCreateResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
                
            }
        }
    }

