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
        public override int SaveChanges() {
            foreach (var entry in ChangeTracker.Entries().Where(x => x.Entity.GetType().GetProperty("CreatedDate") != null)) {
                if (entry.State == EntityState.Added) {
                    entry.Property("CreatedDate").CurrentValue = DateTime.Now;
                } else if (entry.State == EntityState.Modified) {
                    // Ignore the CreatedTime updates on Modified entities. 
                    entry.Property("CreatedDate").IsModified = false;
                }
            }

            foreach (var entry in ChangeTracker.Entries().Where(
                e =>
                    e.Entity.GetType().GetProperty("UpdatedDate") != null &&
                    e.State == EntityState.Modified ||
                    e.State == EntityState.Added)) {
                entry.Property("UpdatedDate").CurrentValue = DateTime.Now;
            }

            return base.SaveChanges();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder
                //.UseLazyLoadingProxies()
                .UseSqlServer("Server=localhost;Database=CheburechnayaDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
