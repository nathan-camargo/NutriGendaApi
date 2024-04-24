using AutoMapper;
using NutriGendaApi.Source.DTOs;

namespace NutriGendaApi.Source.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeamentos para Diet
            CreateMap<Diet, DietDTO>()
                .ReverseMap();

            // Mapeamentos para HealthProfile
            CreateMap<HealthProfile, HealthProfileDTO>()
                .ReverseMap();

            // Mapeamentos para Nutritionist
            CreateMap<Nutritionist, NutritionistDTO>()
                .ReverseMap();

            // Mapeamentos para User
            CreateMap<User, UserDTO>()
                .ReverseMap();
        }
    }
}
