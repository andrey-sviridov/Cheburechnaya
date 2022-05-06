using cheburechnaya_core.Models;
using Microsoft.EntityFrameworkCore;

namespace cheburechnaya_core.Data {
    public class ModelContext : DbContext {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LikeInformation> LikeInformations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=(local);Database=TestDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
