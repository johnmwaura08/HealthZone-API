using HealthZoneAPI.Data;
using HealthZoneAPI.Models;
using HealthZoneAPI.Services.interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthZoneAPI.Services;

public class ExerciseService(HealthzoneDBContext _context) : IExerciseService
{
    public async Task<Step> LogDailyStepCount(int stepCount)
    {

        //var today = DateTime.Now;
        var step = _context.Step.FirstOrDefault(x => x.UpdatedAt.ToUniversalTime().Date == DateTime.Today.ToUniversalTime().Date && x.RecordStatus);

        if (step is not null)
        {

            step.StepCount = stepCount + step.StepCount;
        }
        else
        {

            step = new Step
            {
                StepCount = stepCount,
            };
            _context.Step.Add(step);
        }
        await _context.SaveChangesAsync();
        return step;
    }

    public async Task<List<Step>> GetAllActive()
    {
        return await _context.Step.Where(x => x.RecordStatus).ToListAsync();
    }
}

