using Microsoft.EntityFrameworkCore;
using VizsgaAPI.Models;

namespace VizsgaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
    }
}
