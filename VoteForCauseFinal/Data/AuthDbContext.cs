using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VoteForCauseFinal.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seed roles user, admin superadmin

            //Console.WriteLine(Guid.NewGuid()); inside th c# interactive in views
            var superAdminRoleId = "d8bf5f16-3049-4b2e-9a82-d9fcb4efe380";
            var adminRoleId = "4e7e0d3e-4443-476a-8a18-05e57cccef5b";
            var userRoleId = "d4175650-7871-43b9-a405-fe76bfe6959c";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp =  adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp =  superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp =  userRoleId
                }

            };

            //insert the roles in database
            builder.Entity<IdentityRole>().HasData(roles);

            //seed superadminuser

            var superAdminId = "70bc9075-cacb-44e7-ae8a-a80c1a5277ea";

            var superAdminUser = new IdentityUser

            {
                UserName = "superadmin@voteforcauses.com",
                Email = "superadmin@voteforcauses.com",
                NormalizedEmail = "superadmin@voteforcauses.com".ToUpper(),
                NormalizedUserName = "superadmin@voteforcauses.com".ToUpper(),
                Id = superAdminId,
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "SuperAdmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);


            // add all roles to superadmin
            //requires a string
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>()
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId

                },
                 new IdentityUserRole<string>()
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId

                },
                  new IdentityUserRole<string>()
                {
                    RoleId = userRoleId,
                    UserId = superAdminId

                },
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

        }

    }
}
