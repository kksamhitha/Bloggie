using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //seed roles (user, Admin, super Admin)
            var adminRoleId = "44e1151b-361a-4513-98aa-af49e17f353b";
            var superAdminRoleId = "172c9c35-4c32-478b-be97-265fac32dc22";
            var userRoleId = "04741968-c732-4ef2-8a22-351e1b64568f";
            var superAdminUserId = "92cbf4fe-15fa-4973-9e3d-abbe980866c2";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                { 
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                }                
            };
            builder.Entity<IdentityRole>().HasData(roles);

            //seed Super Admin User role which will have all the access of user, Admin and super Admin
            var superAdminUser = new IdentityUser
            {
                UserName = "superadminuser@bloggie.com",
                Email = "superadminuser@bloggie.com",
                NormalizedEmail = "superadminuser@bloggie.com".ToUpper(),
                NormalizedUserName = "superadminuser@bloggie.com",
                Id = superAdminUserId
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser,"superadmin@123");
            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add all roles to super Admin
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId =adminRoleId,
                    UserId = superAdminUserId
                },
                new IdentityUserRole<string>
                {
                    RoleId =superAdminRoleId,
                    UserId = superAdminUserId
                },
                new IdentityUserRole<string>
                {
                    RoleId =userRoleId,
                    UserId = superAdminUserId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
