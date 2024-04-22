using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NutriGendaApi.Source.Data;
using NutriGendaApi.Source.DTOs;

namespace NutriGendaApi.Source.Services
{
    public class NutritionistService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public NutritionistService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria um novo nutricionista no banco de dados.
        /// </summary>
        /// <param name="nutritionistDto">O DTO do nutricionista a ser criado.</param>
        /// <returns>O DTO do nutricionista criado com seu ID.</returns>
        public async Task<NutritionistDTO> CreateNutritionist(NutritionistDTO nutritionistDto)
        {
            var nutritionist = _mapper.Map<Nutritionist>(nutritionistDto);
            _context.Nutritionists.Add(nutritionist);
            await _context.SaveChangesAsync();
            return _mapper.Map<NutritionistDTO>(nutritionist);
        }

        /// <summary>
        /// Busca um nutricionista pelo ID.
        /// </summary>
        /// <param name="id">ID do nutricionista.</param>
        /// <returns>O DTO do nutricionista encontrado ou null se não existir.</returns>
        public async Task<NutritionistDTO> GetNutritionistById(Guid id)
        {
            var nutritionist = await _context.Nutritionists.FindAsync(id);
            return _mapper.Map<NutritionistDTO>(nutritionist);
        }

        /// <summary>
        /// Atualiza informações de um nutricionista existente.
        /// </summary>
        /// <param name="nutritionistDto">DTO do nutricionista com informações atualizadas.</param>
        /// <returns>O DTO do nutricionista atualizado.</returns>
        public async Task<NutritionistDTO> UpdateNutritionist(NutritionistDTO nutritionistDto)
        {
            var nutritionist = await _context.Nutritionists.FindAsync(nutritionistDto.Id);
            if (nutritionist == null)
            {
                return null;
            }
            _mapper.Map(nutritionistDto, nutritionist);
            _context.Entry(nutritionist).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<NutritionistDTO>(nutritionist);
        }

        /// <summary>
        /// Deleta um nutricionista pelo ID.
        /// </summary>
        /// <param name="id">ID do nutricionista a ser deletado.</param>
        /// <returns>True se o nutricionista foi deletado com sucesso, false caso contrário.</returns>
        public async Task<bool> DeleteNutritionist(Guid id)
        {
            var nutritionist = await _context.Nutritionists.FindAsync(id);
            if (nutritionist == null)
            {
                return false;
            }
            _context.Nutritionists.Remove(nutritionist);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Lista todos os nutricionistas registrados no sistema.
        /// </summary>
        /// <returns>Lista de DTOs de nutricionistas.</returns>
        public async Task<List<NutritionistDTO>> GetAllNutritionists()
        {
            var nutritionists = await _context.Nutritionists.ToListAsync();
            return _mapper.Map<List<NutritionistDTO>>(nutritionists);
        }
    }
}
