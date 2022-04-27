using cheburechnaya_core.Models;
using Microsoft.EntityFrameworkCore;

namespace cheburechnaya_core.Data {
    public class ModelContext : DbContext {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("Server=(local);Database=TestDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
