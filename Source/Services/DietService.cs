using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NutriGendaApi.Source.Data;
using NutriGendaApi.Source.DTOs;

namespace NutriGendaApi.Source.Services
{
    public class DietService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DietService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona uma nova dieta ao banco de dados.
        /// </summary>
        /// <param name="diet">Objeto Diet contendo todas as informações necessárias.</param>
        /// <returns>A dieta criada com seu ID após a inserção no banco de dados.</returns>
        public async Task<DietDTO> CreateDiet(DietDTO dietDto)
        {
            var diet = _mapper.Map<Diet>(dietDto);
            _context.Diets.Add(diet);
            await _context.SaveChangesAsync();
            return _mapper.Map<DietDTO>(diet);
        }

        /// <summary>
        /// Busca uma dieta específica pelo seu ID.
        /// </summary>
        /// <param name="id">ID da dieta.</param>
        /// <returns>A dieta correspondente ou null se não encontrada.</returns>
        public async Task<DietDTO> GetDietById(Guid id)
        {
            var diet = await _context.Diets.FindAsync(id);
            return _mapper.Map<DietDTO>(diet);
        }

        /// <summary>
        /// Atualiza uma dieta existente com novos dados.
        /// </summary>
        /// <param name="diet">Objeto Diet atualizado.</param>
        /// <returns>A dieta atualizada.</returns>
        public async Task<DietDTO> UpdateDiet(DietDTO dietDto)
        {
            var diet = await _context.Diets.FindAsync(dietDto.Id);
            if (diet == null)
            {
                return null;
            }
            _mapper.Map(dietDto, diet);
            _context.Entry(diet).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<DietDTO>(diet);
        }

        /// <summary>
        /// Remove uma dieta do banco de dados.
        /// </summary>
        /// <param name="id">ID da dieta a ser removida.</param>
        /// <returns>Confirmação de que a dieta foi removida.</returns>
        public async Task<bool> DeleteDiet(Guid id)
        {
            var diet = await _context.Diets.FindAsync(id);
            if (diet == null)
            {
                return false;
            }
            _context.Diets.Remove(diet);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Retorna uma lista de todas as dietas cadastradas.
        /// </summary>
        /// <returns>Lista de dietas.</returns>
        public async Task<List<DietDTO>> GetAllDiets()
        {
            var diets = await _context.Diets.ToListAsync();
            return _mapper.Map<List<DietDTO>>(diets);
        }

        /// <summary>
        /// Busca todas as dietas associadas a um usuário específico.
        /// </summary>
        /// <param name="userId">ID do usuário.</param>
        /// <returns>Lista de dietas pertencentes ao usuário.</returns>
        public async Task<List<DietDTO>> GetDietsByUserId(Guid userId)
        {
            var diets = await _context.Diets.Where(d => d.UserId == userId).ToListAsync();
            return _mapper.Map<List<DietDTO>>(diets);
        }
    }
}
