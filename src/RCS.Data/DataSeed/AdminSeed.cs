using Microsoft.AspNetCore.Identity;
using RCS.Data.Identity.Entities;

namespace RCS.Data.DataSeed
{
    public static class AdminSeed
    {
        public static ApplicationUser[] GetUsers
        {
            get
            {
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                var user = new ApplicationUser()
                {
                    FirstName = "Nakib",
                    Email = "nakib123@gmail.com",
                    UserName = "nakib123@gmail.com",
                    NormalizedEmail = "NAKIB123@GMAIL.COM",
                    NormalizedUserName = "NAKIB123@GMAIL.COM",
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = false,
                };

                var passwordHash = passwordHasher.HashPassword(user, "Nakib#12345");
                user.PasswordHash = passwordHash;

                return new ApplicationUser[] { user };
            }
        }
    }
}
