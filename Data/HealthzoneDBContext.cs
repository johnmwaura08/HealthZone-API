using HealthZoneAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthZoneAPI.Data
{
    public class HealthzoneDBContext(DbContextOptions<HealthzoneDBContext> options) : DbContext(options), IHealthzoneDBContext
    {
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            PrepareEntitiesForSaving();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            PrepareEntitiesForSaving();
            return base.SaveChanges();
        }
       

        private void PrepareEntitiesForSaving()
        {
            //Get all entities being added or changed
            var entries = ChangeTracker.Entries().Where(e =>
                e.State == EntityState.Added
                || e.State == EntityState.Modified);


            foreach (var entityEntry in entries)
            {
                entityEntry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                    entityEntry.Property("RecordStatus").CurrentValue = true;
                }
            }
        }


        public DbSet<Weight> Weight { get; set; }

        public DbSet<Step> Step { get; set; }

        public DbSet<WorkoutCategory> WorkoutCategory { get; set; }

        public DbSet<Workout> Workout { get; set; }
    }


}
