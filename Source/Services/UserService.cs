using Microsoft.EntityFrameworkCore;
using NutriGendaApi.src.Data;

namespace NutriGendaApi.src.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="user">Dados do usuário a ser criado.</param>
        /// <returns>O usuário criado com ID.</returns>
        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Busca um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>O usuário encontrado ou null se não existir.</returns>
        public async Task<User> GetUserById(Guid id)
        {
            return await _context.Users
                                 .Include(u => u.Nutritionist)
                                 .Include(u => u.HealthProfile)
                                 .Include(u => u.Diets)
                                 .FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        /// <param name="user">Dados atualizados do usuário.</param>
        /// <returns>O usuário atualizado.</returns>
        public async Task<User> UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Deleta um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário a ser deletado.</param>
        /// <returns>True se deletado com sucesso, false se não encontrado.</returns>
        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Busca um usuário pelo email.
        /// </summary>
        /// <param name="email">Email do usuário a ser encontrado.</param>
        /// <returns>O usuário encontrado ou null se não existir.</returns>
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users
                                 .Include(u => u.Nutritionist)
                                 .Include(u => u.HealthProfile)
                                 .Include(u => u.Diets)
                                 .FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// Lista todos os usuários registrados.
        /// </summary>
        /// <returns>Lista de todos os usuários.</returns>
        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users
                                 .Include(u => u.Nutritionist)
                                 .Include(u => u.HealthProfile)
                                 .Include(u => u.Diets)
                                 .ToListAsync();
        }
    }
}
