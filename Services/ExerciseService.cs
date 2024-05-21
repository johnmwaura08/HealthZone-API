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


    public async Task<List<WorkoutCategory>> GetAllWorkoutCategories()
    {
        return await _context.WorkoutCategory.Where(x => x.RecordStatus).ToListAsync();
    }

    public async Task<WorkoutCategory> GetWorkoutCategoryById(int id)
    {
        return await _context.WorkoutCategory.FirstOrDefaultAsync(x => x.Id == id && x.RecordStatus) ?? throw new Exception("Workout Category not found");
    }

    public async Task<WorkoutCategory> CreateWorkoutCategory(string name, string description, string type)
    {
        var workoutCategory = new WorkoutCategory
        {
            CategoryName = name,
            Description = description,
            CategoryType = type
        };
        _context.WorkoutCategory.Add(workoutCategory);
        await _context.SaveChangesAsync();
        return workoutCategory;
    }

    public async Task<WorkoutCategory> UpdateWorkoutCategory(int id, string name, string description, string type)
    {
        var workoutCategory = await _context.WorkoutCategory.FirstOrDefaultAsync(x => x.Id == id && x.RecordStatus) ?? throw new Exception("Workout Category not found");
        workoutCategory.CategoryName = name;
        workoutCategory.Description = description;
        workoutCategory.CategoryType = type;
        await _context.SaveChangesAsync();
        return workoutCategory;
    }

    public async Task<WorkoutCategory> DeleteWorkoutCategory(int id)
    {
        var workoutCategory = await _context.WorkoutCategory.FirstOrDefaultAsync(x => x.Id == id && x.RecordStatus) ?? throw new Exception("Workout Category not found");
        workoutCategory.RecordStatus = false;
        await _context.SaveChangesAsync();
        return workoutCategory;
    }


    public async Task<Workout> CreateWorkout(int weight, int categoryId, int reps, int sets, string comments)

    {
        var workout = new Workout
        {
            WorkoutCategoryId = categoryId,
            Comments = comments,
            Reps = reps,
            Sets = sets,
            WeightInPounds = weight
        };
        _context.Workout.Add(workout);
        await _context.SaveChangesAsync();
        return workout;
    }

    public async Task<Workout> UpdateWorkout(int id, int weight, int categoryId, string comments)
    {
        var workout = await _context.Workout.FirstOrDefaultAsync(x => x.Id == id && x.RecordStatus) ?? throw new Exception("Workout not found");
        workout.WeightInPounds = weight;
        workout.WorkoutCategoryId = categoryId;
        workout.Comments = comments;
        await _context.SaveChangesAsync();
        return workout;
    }

    public async Task<Workout> DeleteWorkout(int id)
    {
        var workout = await _context.Workout.FirstOrDefaultAsync(x => x.Id == id && x.RecordStatus) ?? throw new Exception("Workout not found");
        workout.RecordStatus = false;
        await _context.SaveChangesAsync();
        return workout;
    }

    public async Task<List<Workout>> GetAllWorkouts()
    {
        return await _context.Workout.Where(x => x.RecordStatus).Include(x => x.WorkoutCategory).ToListAsync();
    }

}

