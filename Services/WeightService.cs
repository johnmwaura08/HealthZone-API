using HealthZoneAPI.Data;
using HealthZoneAPI.Models;
using HealthZoneAPI.Services.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthZoneAPI.Services
{
    public class WeightService(HealthzoneDBContext _context): IWeightService
    {
       public async Task<List<Weight>> GetAllActive()
        {

            return await _context.Weight.Where(x => x.RecordStatus).ToListAsync();

        }

        public async Task<Weight> CreateDailyWeightValue(double val)
        {

            var weight = new Weight
            {
                WeightValue = val
            };

            _context.Weight.Add(weight);
            await _context.SaveChangesAsync();
            return weight;


        }

    }
}
