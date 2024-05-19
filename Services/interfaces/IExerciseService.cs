using HealthZoneAPI.Models;

namespace HealthZoneAPI.Services.interfaces
{
    public interface IExerciseService
    {

        Task<Step> LogDailyStepCount(int stepCount);

        Task<List<Step>> GetAllActive();

        Task<List<WorkoutCategory>> GetAllWorkoutCategories();

        Task<WorkoutCategory> GetWorkoutCategoryById(int id);

        Task<WorkoutCategory> CreateWorkoutCategory(string name, string description, string type);

        Task<List<Workout>> GetAllWorkouts();

        Task<Workout> CreateWorkout(string name, int categoryId,
        int reps, int sets,
         string comments);




    }
}
