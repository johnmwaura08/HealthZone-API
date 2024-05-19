using HealthZoneAPI.Models;
using HealthZoneAPI.Models.Requests;
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

        [HttpGet("get-all-steps")]
        public async Task<ActionResult> GetAllActive()
        {
            var res = await _exerciseService.GetAllActive();
            return Ok(res);
        }



        [HttpPost("create-workout-category")]
        public async Task<ActionResult> CreateWorkoutCategory([FromBody] CreateWorkoutCategoryRequest request)
        {
            var res = await _exerciseService.CreateWorkoutCategory(
                request.Name, request.Description, request.Type);
            return Ok(res);


        }


        [HttpGet("get-all-workout-category-types")]
        public ActionResult GetAllWorkoutCategoryTypes()
        {
            var res = new List<string> {
                CategoryType.PushDay.ToString(),
                CategoryType.PullDay.ToString(),
                CategoryType.LegDay.ToString(),
                CategoryType.Cardio.ToString(),
                CategoryType.Abs.ToString()
            };

            return Ok(res);
        }

        [HttpGet("get-all-workout-categories")]
        public async Task<ActionResult> GetAllWorkoutCategories()
        {
            var res = await _exerciseService.GetAllWorkoutCategories();
            return Ok(res);
        }

        [HttpGet("get-workout-category-by-id")]
        public async Task<ActionResult> GetWorkoutCategoryById(int id)
        {
            var res = await _exerciseService.GetWorkoutCategoryById(id);
            return Ok(res);
        }


        [HttpPost("create-workout")]
        public async Task<ActionResult> CreateWorkout([FromBody] CreateWorkoutRequest request)
        {
            var res = await _exerciseService.CreateWorkout(
                request.Name, request.CategoryId, request.Reps, request.Sets, request.Comments);
            return Ok(res);
        }

        [HttpGet("get-all-workouts")]
        public async Task<ActionResult> GetAllWorkouts()
        {
            var res = await _exerciseService.GetAllWorkouts();
            return Ok(res);
        }


    }
}
