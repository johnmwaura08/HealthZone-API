using HealthZoneAPI.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthZoneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController(IExerciseService _exerciseService) : ControllerBase
    {
      

        [HttpPost("log-daily-step-count")]
        public async Task<ActionResult> LogDailyStepCount(int stepCount)
        {
            var step = await _exerciseService.LogDailyStepCount(stepCount);
            return Ok(step);
        }

        [HttpGet("get-all-active")]
        public async Task<ActionResult> GetAllActive()
        {
            var res = await _exerciseService.GetAllActive();
            return Ok(res);
        }
    }
}
