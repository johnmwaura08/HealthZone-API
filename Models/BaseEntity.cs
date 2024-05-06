namespace HealthZoneAPI.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public bool RecordStatus { get; set; } 

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
