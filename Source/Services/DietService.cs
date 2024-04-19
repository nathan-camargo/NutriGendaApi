using Microsoft.EntityFrameworkCore;
using NutriGendaApi.src.Data;

namespace NutriGendaApi.src.Services
{
    public class DietService
    {
        private readonly AppDbContext _context;

        public DietService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona uma nova dieta ao banco de dados.
        /// </summary>
        /// <param name="diet">Objeto Diet contendo todas as informações necessárias.</param>
        /// <returns>A dieta criada com seu ID após a inserção no banco de dados.</returns>
        public async Task<Diet> CreateDiet(Diet diet)
        {
            _context.Diets.Add(diet);
            await _context.SaveChangesAsync();
            return diet;
        }

        /// <summary>
        /// Busca uma dieta específica pelo seu ID.
        /// </summary>
        /// <param name="id">ID da dieta.</param>
        /// <returns>A dieta correspondente ou null se não encontrada.</returns>
        public async Task<Diet> GetDietById(Guid id)
        {
            return await _context.Diets.FindAsync(id);
        }

        /// <summary>
        /// Atualiza uma dieta existente com novos dados.
        /// </summary>
        /// <param name="diet">Objeto Diet atualizado.</param>
        /// <returns>A dieta atualizada.</returns>
        public async Task<Diet> UpdateDiet(Diet diet)
        {
            _context.Entry(diet).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return diet;
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
        public async Task<List<Diet>> GetAllDiets()
        {
            return await _context.Diets.ToListAsync();
        }

        /// <summary>
        /// Busca todas as dietas associadas a um usuário específico.
        /// </summary>
        /// <param name="userId">ID do usuário.</param>
        /// <returns>Lista de dietas pertencentes ao usuário.</returns>
        public async Task<List<Diet>> GetDietsByUserId(Guid userId)
        {
            return await _context.Diets.Where(d => d.UserId == userId).ToListAsync();
        }
    }
}
