using HealthZoneAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthZoneAPI.Utils
{
    public class MigrationsHelper
    {
        public static async Task ManageDataAsync(IServiceProvider svcProvider)
        {
            //Service: An instance of db context
            var dbContextSvc = svcProvider.GetRequiredService<HealthzoneDBContext>();

            //Migration: This is the programmatic equivalent to Update-Database
            await dbContextSvc.Database.MigrateAsync();
        }
    }
}
