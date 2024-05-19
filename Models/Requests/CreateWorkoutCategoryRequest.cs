using System.ComponentModel.DataAnnotations;

namespace HealthZoneAPI.Models.Requests
{
    public class CreateWorkoutCategoryRequest
    {

        [Required]

        public string Name { get; set; } = string.Empty;

        [Required]

        public string Description { get; set; } = "";

        [Required]
        [RegularExpression("Cardio|LegDay|PushDay|PullDay|Abs")]
        public string Type { get; set; } = CategoryType.PushDay;
    }

    public class CreateWorkoutRequest
    {

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }
        public string Comments { get; set; } = "";

public int Reps { get; set; } = 0;

        public int Sets { get; set; } = 0;

    }
}