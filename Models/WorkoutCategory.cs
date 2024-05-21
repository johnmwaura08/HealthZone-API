using System.Reflection.Metadata;

namespace HealthZoneAPI.Models;


public static class CategoryType
{
    public static string Cardio = "Cardio";

    public const string LegDay = "LegDay";

    public const string PushDay = "PushDay";

    public const string PullDay = "PullDay";

    public const string Abs = "Abs";

}
public class WorkoutCategory : BaseEntity
{


    public string CategoryName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string CategoryType { get; set; } = string.Empty;
}