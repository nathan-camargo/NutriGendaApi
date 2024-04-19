namespace NutriGendaApi.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public Guid NutritionistId { get; set; }
    }
}
