// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo
namespace WoWForDummies.Data.Seeder
{
    using WoWForDummies.Common;
    using Models;
    using System.Linq;
    using Microsoft.AspNetCore.Identity;
    using Contracts;

    public class Seeder : ISeeder
    {
        private readonly WoWForDummiesDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public Seeder(WoWForDummiesDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public void SeedDatabase()
        {
            _dbContext.Database.EnsureCreated();

            // Seed user roles

            #region Roles

            if (!_dbContext.Roles.Any())
            {
                _dbContext.Roles.Add(new IdentityRole
                {
                    Name = GlobalConstants.AdminRole,
                    NormalizedName = GlobalConstants.AdminRole.ToUpper()
                });

                _dbContext.Roles.Add(new IdentityRole
                {
                    Name = GlobalConstants.UserRole,
                    NormalizedName = GlobalConstants.UserRole.ToUpper()
                });
            }

            #endregion

            // Seed users

            if (!_dbContext.Users.Any())
            {
                #region AdminUser

                const string adminPassword = "admin123";
                var admin = new User()
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                };

                _userManager.CreateAsync(admin, adminPassword).Wait();
                _userManager.AddToRoleAsync(admin, GlobalConstants.AdminRole).Wait();

                #endregion
            }

            _dbContext.SaveChangesAsync();
        }
    }
}