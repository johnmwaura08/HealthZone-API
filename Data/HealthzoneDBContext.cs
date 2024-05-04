using HealthZoneAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthZoneAPI.Data
{
    public class HealthzoneDBContext(DbContextOptions<HealthzoneDBContext> options) : DbContext(options)
    {

        public DbSet<Todo> Todo { get; set; }
    }


}
