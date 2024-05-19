namespace HealthZoneAPI.Models;


public class Workout: BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public int WorkoutCategoryId { get; set; }

    public WorkoutCategory WorkoutCategory { get; set; }

    public int Reps { get; set; } = 0;

    public int Sets { get; set; } = 0;

public string Comments { get; set; } = string.Empty;


}