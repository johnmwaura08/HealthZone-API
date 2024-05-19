using HealthZoneAPI.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace HealthZoneAPI.Data
{
    public interface IHealthzoneDBContext
    {
        EntityEntry Entry([NotNullAttribute] object entity);
        EntityEntry<TEntity> Update<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DbSet<Weight> Weight { get; set; }

        DbSet<Step> Step { get; set; }

        DbSet<WorkoutCategory> WorkoutCategory { get; set; }

        DbSet<Workout> Workout { get; set; }
    }
}
