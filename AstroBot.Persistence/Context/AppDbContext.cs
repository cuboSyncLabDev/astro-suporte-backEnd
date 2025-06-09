using AstroBot.Domain.Entities;
using AstroBot.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace AstroBot.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserMapping());
        }
    }
}
