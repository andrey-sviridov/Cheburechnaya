using cheburechnaya_core.Models;
using Microsoft.EntityFrameworkCore;

namespace cheburechnaya_core.Data {
    public class ModelContext : DbContext {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);


            //modelBuilder.ApplyConfiguration(new PostConfiguration());

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder
                //.UseLazyLoadingProxies()
                .UseSqlServer("Server=localhost;Database=TestchebDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
