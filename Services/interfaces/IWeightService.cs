using HealthZoneAPI.Models;

namespace HealthZoneAPI.Services.interfaces
{
    public interface IWeightService
    {

        Task<List<Weight>> GetAllActive();

        Task<Weight> CreateDailyWeightValue(double val);
    }
}
