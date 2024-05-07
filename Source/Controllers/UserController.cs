﻿using Microsoft.AspNetCore.Mvc;
using NutriGendaApi.Source.DTOs;
using NutriGendaApi.Source.Services;


namespace NutriGendaApi.Source.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retorna todos os usuários cadastrados.
        /// </summary>
        /// <returns>Uma lista de DTOs de usuários.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        /// <summary>
        /// Retorna um usuário específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do usuário.</param>
        /// <returns>O DTO do usuário encontrado ou NotFound se não existir.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="userDto">DTO do usuário a ser criado.</param>
        /// <returns>O DTO do usuário criado.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDto)
        {
            var createdUser = await _userService.CreateUser(userDto);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        /// <param name="id">O ID do usuário a ser atualizado.</param>
        /// <param name="userDto">DTO com dados atualizados do usuário.</param>
        /// <returns>NoContent se a atualização for bem-sucedida, NotFound se não encontrado.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserDTO userDto)
        {
            if (id != userDto.Id)
                return BadRequest("ID mismatch");

            var existingUser = await _userService.GetUserById(id);
            if (existingUser == null)
                return NotFound();

            await _userService.UpdateUser(userDto);
            return NoContent();
        }

        /// <summary>
        /// Deleta um usuário pelo ID.
        /// </summary>
        /// <param name="id">O ID do usuário a ser deletado.</param>
        /// <returns>NoContent se deletado com sucesso, NotFound se não encontrado.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var success = await _userService.DeleteUser(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Busca um usuário pelo email.
        /// </summary>
        /// <param name="email">Email do usuário a ser encontrado.</param>
        /// <returns>O DTO do usuário encontrado ou NotFound se não existir.</returns>
        [HttpGet("byemail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmail(email);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
    }
}
