﻿namespace NutriGendaApi.Source.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid NutritionistId { get; set; }
    }
}
