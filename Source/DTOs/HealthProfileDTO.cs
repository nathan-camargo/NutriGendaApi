namespace NutriGendaApi.src.DTOs
{
    public class HealthProfileDTO
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string Comments { get; set; }
    }
}
