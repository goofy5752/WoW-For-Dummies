namespace WoWForDummies.Data
{
    using Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class WoWForDummiesDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public WoWForDummiesDbContext(DbContextOptions<WoWForDummiesDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
