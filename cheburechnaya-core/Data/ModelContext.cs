using cheburechnaya_core.Models;
using Microsoft.EntityFrameworkCore;

namespace cheburechnaya_core.Data {
    public class ModelContext : DbContext {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().HasOne(x => x.Author).WithMany(x => x.CreatedPosts).HasForeignKey("AuthorId").OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.ApplyConfiguration(new PostConfiguration());

        }
        public override int SaveChanges() {
            foreach (var changedEntity in ChangeTracker.Entries()) {
                if (changedEntity.Entity is IEntityDate entity) {
                    switch (changedEntity.State) {
                        case EntityState.Added:
                            entity.CreatedDate = DateTime.Now;
                            entity.UpdatedDate = DateTime.Now;
                            break;

                        case EntityState.Modified:
                            //Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                            entity.UpdatedDate = DateTime.Now;
                            break;
                    }
                }
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
