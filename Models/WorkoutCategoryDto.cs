namespace HealthZoneAPI.Models
{
    public class WorkoutCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CategoryType { get; set; } = string.Empty;
    }
}