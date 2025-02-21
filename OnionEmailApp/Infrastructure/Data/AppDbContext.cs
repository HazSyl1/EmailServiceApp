using Microsoft.EntityFrameworkCore;
using OnionEmailApp.Domain.Entities;

namespace OnionEmailApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Email> Emails { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}