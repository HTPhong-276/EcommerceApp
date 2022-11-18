using Domain.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Phong",
                    Email = "phong@gmail.com",
                    UserName = "phong@gmail.com",
                    Address = new Address
                    {
                        FirstName = "Phong",
                        LastName = "Thai",
                        Street = "Street here",
                        City = "City here",
                        State = "State here",
                        ZipCode = "123321"
                    }
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");

            }
        }
    }
}
