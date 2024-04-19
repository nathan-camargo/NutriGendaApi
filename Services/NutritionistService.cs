using Microsoft.EntityFrameworkCore;
using NutriGendaApi.Data;

namespace NutriGendaApi.Services
{
    public class NutritionistService
    {
        private readonly AppDbContext _context;

        public NutritionistService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria um novo nutricionista no banco de dados.
        /// </summary>
        /// <param name="nutritionist">O nutricionista a ser criado.</param>
        /// <returns>O nutricionista criado com seu ID.</returns>
        public async Task<Nutritionist> CreateNutritionist(Nutritionist nutritionist)
        {
            _context.Nutritionists.Add(nutritionist);
            await _context.SaveChangesAsync();
            return nutritionist;
        }

        /// <summary>
        /// Busca um nutricionista pelo ID.
        /// </summary>
        /// <param name="id">ID do nutricionista.</param>
        /// <returns>O nutricionista encontrado ou null se não existir.</returns>
        public async Task<Nutritionist> GetNutritionistById(Guid id)
        {
            return await _context.Nutritionists.FindAsync(id);
        }

        /// <summary>
        /// Atualiza informações de um nutricionista existente.
        /// </summary>
        /// <param name="nutritionist">Nutricionista com informações atualizadas.</param>
        /// <returns>O nutricionista atualizado.</returns>
        public async Task<Nutritionist> UpdateNutritionist(Nutritionist nutritionist)
        {
            _context.Entry(nutritionist).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return nutritionist;
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
        /// <returns>Lista de nutricionistas.</returns>
        public async Task<List<Nutritionist>> GetAllNutritionists()
        {
            return await _context.Nutritionists.ToListAsync();
        }
    }
}
