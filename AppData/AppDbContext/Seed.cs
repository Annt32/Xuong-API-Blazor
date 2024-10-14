using AppData.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace XuongTT_API.Model
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            //static Roles
            List<Role> roles = new List<Role>()
            {
                new Role{Id = Guid.NewGuid(), Name = "Admin", NormalizedName="ADMIN",Status = 1 ,CreatedAt = DateTime.Now,UpdatedAt = DateTime.Now},
                new Role{Id = Guid.NewGuid(), Name = "Staff", NormalizedName="STAFF",Status = 1 ,CreatedAt = DateTime.Now,UpdatedAt = DateTime.Now},
                new Role{Id = Guid.NewGuid(), Name = "Customer", NormalizedName="CUSTOMER",Status = 1 ,CreatedAt = DateTime.Now,UpdatedAt = DateTime.Now},
            };
            builder.Entity<IdentityRole<Guid>>().HasData(roles);
            //static Users
            var passwordHasher = new PasswordHasher<User>();
            List<User> users = new List<User>()
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    FullName = "khoong",
                    Address = "khong",
                    Status = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "staff",
                    NormalizedUserName = "STAFF",
                    Email = "staff@gmail.com",
                    NormalizedEmail = "STAFF@GMAIL.COM",
                    FullName = "khoong",
                    Address = "khong",
                    Status = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "user1",
                    NormalizedUserName = "USER1",
                    Email = "user1@gmail.com",
                    NormalizedEmail = "USER1@GMAIL.COM",
                    FullName = "khoong",
                    Address = "khong",
                    Status = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    SecurityStamp = Guid.NewGuid().ToString()
                }
            };

            builder.Entity<User>().HasData(users);

            // static userRole

            List<IdentityUserRole<Guid>> userRoles = new List<IdentityUserRole<Guid>>();
                users[0].PasswordHash = passwordHasher.HashPassword(users[0], "Pa$$word1");
                users[1].PasswordHash = passwordHasher.HashPassword(users[1], "Pa$$word1");
                users[2].PasswordHash = passwordHasher.HashPassword(users[2], "Pa$$word1");
                
                userRoles.Add(new IdentityUserRole<Guid> { UserId = users[0].Id,RoleId = roles.First(q => q.Name == "Admin").Id });
                userRoles.Add(new IdentityUserRole<Guid> { UserId = users[1].Id,RoleId = roles.First(q => q.Name == "Staff").Id });
                userRoles.Add(new IdentityUserRole<Guid> { UserId = users[2].Id,RoleId = roles.First(q => q.Name == "Customer").Id });

            builder.Entity<IdentityUserRole<Guid>>().HasData(userRoles);
        }
    }
}
