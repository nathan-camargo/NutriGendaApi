﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NutriGendaApi.Source.Data;
using NutriGendaApi.Source.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutriGendaApi.Source.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="userDto">DTO do usuário a ser criado.</param>
        /// <returns>DTO do usuário criado com ID.</returns>
        public async Task<UserDTO> CreateUser(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }

        /// <summary>
        /// Busca um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>DTO do usuário encontrado ou null se não existir.</returns>
        public async Task<UserDTO> GetUserById(Guid id)
        {
            var user = await _context.Users
                                     .Include(u => u.Nutritionist)
                                     .Include(u => u.HealthProfile)
                                     .Include(u => u.Diets)
                                     .FirstOrDefaultAsync(u => u.Id == id);
            return _mapper.Map<UserDTO>(user);
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        /// <param name="userDto">DTO do usuário com dados atualizados.</param>
        /// <returns>DTO do usuário atualizado.</returns>
        public async Task<UserDTO> UpdateUser(UserDTO userDto)
        {
            var user = await _context.Users.FindAsync(userDto.Id);
            if (user == null)
            {
                return null;
            }
            _mapper.Map(userDto, user);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
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
        /// <returns>DTO do usuário encontrado ou null se não existir.</returns>
        public async Task<UserDTO> GetUserByEmail(string email)
        {
            var user = await _context.Users
                                     .Include(u => u.Nutritionist)
                                     .Include(u => u.HealthProfile)
                                     .Include(u => u.Diets)
                                     .FirstOrDefaultAsync(u => u.Email == email);
            return _mapper.Map<UserDTO>(user);
        }

        /// <summary>
        /// Lista todos os usuários registrados.
        /// </summary>
        /// <returns>Lista de DTOs de todos os usuários.</returns>
        public async Task<List<UserDTO>> GetAllUsers()
        {
            var users = await _context.Users
                                      .Include(u => u.Nutritionist)
                                      .Include(u => u.HealthProfile)
                                      .Include(u => u.Diets)
                                      .ToListAsync();
            return _mapper.Map<List<UserDTO>>(users);
        }
    }
}
