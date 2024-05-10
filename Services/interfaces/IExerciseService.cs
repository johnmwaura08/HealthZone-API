using HealthZoneAPI.Models;

namespace HealthZoneAPI.Services.interfaces
{
    public interface IExerciseService
    {

        Task<Step> LogDailyStepCount(int stepCount);

        Task<List<Step>> GetAllActive();
    }
}
