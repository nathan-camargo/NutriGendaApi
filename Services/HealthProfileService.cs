using Microsoft.EntityFrameworkCore;
using NutriGendaApi.Data;

namespace NutriGendaApi.Services
{
    public class HealthProfileService
    {
        private readonly AppDbContext _context;

        public HealthProfileService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria um novo perfil de saúde.
        /// </summary>
        /// <param name="healthProfile">Dados do perfil de saúde a ser criado.</param>
        /// <returns>O perfil de saúde criado com ID.</returns>
        public async Task<HealthProfile> CreateHealthProfile(HealthProfile healthProfile)
        {
            _context.HealthProfiles.Add(healthProfile);
            await _context.SaveChangesAsync();
            return healthProfile;
        }

        /// <summary>
        /// Busca um perfil de saúde pelo ID.
        /// </summary>
        /// <param name="id">ID do perfil de saúde.</param>
        /// <returns>O perfil de saúde encontrado ou null se não existir.</returns>
        public async Task<HealthProfile> GetHealthProfileById(Guid id)
        {
            return await _context.HealthProfiles.FindAsync(id);
        }

        /// <summary>
        /// Atualiza um perfil de saúde existente.
        /// </summary>
        /// <param name="healthProfile">Perfil de saúde com dados atualizados.</param>
        /// <returns>O perfil de saúde atualizado.</returns>
        public async Task<HealthProfile> UpdateHealthProfile(HealthProfile healthProfile)
        {
            _context.Entry(healthProfile).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return healthProfile;
        }

        /// <summary>
        /// Deleta um perfil de saúde pelo ID.
        /// </summary>
        /// <param name="id">ID do perfil de saúde a ser deletado.</param>
        /// <returns>True se deletado com sucesso, false se não encontrado.</returns>
        public async Task<bool> DeleteHealthProfile(Guid id)
        {
            var healthProfile = await _context.HealthProfiles.FindAsync(id);
            if (healthProfile == null)
            {
                return false;
            }

            _context.HealthProfiles.Remove(healthProfile);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Lista todos os perfis de saúde registrados.
        /// </summary>
        /// <returns>Lista de todos os perfis de saúde.</returns>
        public async Task<List<HealthProfile>> GetAllHealthProfiles()
        {
            return await _context.HealthProfiles.ToListAsync();
        }

        /// <summary>
        /// Busca perfis de saúde associados a um usuário específico.
        /// </summary>
        /// <param name="userId">ID do usuário relacionado.</param>
        /// <returns>Lista de perfis de saúde do usuário.</returns>
        public async Task<List<HealthProfile>> GetHealthProfilesByUserId(Guid userId)
        {
            return await _context.HealthProfiles.Where(hp => hp.UserId == userId).ToListAsync();
        }
    }
}
