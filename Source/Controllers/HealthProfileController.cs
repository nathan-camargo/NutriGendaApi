using Microsoft.AspNetCore.Mvc;
using NutriGendaApi.Source.DTOs;
using NutriGendaApi.Source.Services;
using System;
using System.Threading.Tasks;

namespace NutriGendaApi.Source.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthProfileController : ControllerBase
    {
        private readonly HealthProfileService _healthProfileService;

        public HealthProfileController(HealthProfileService healthProfileService)
        {
            _healthProfileService = healthProfileService;
        }

        /// <summary>
        /// Retorna todos os perfis de saúde cadastrados.
        /// </summary>
        /// <returns>Uma lista de DTOs de perfis de saúde.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllHealthProfiles()
        {
            var healthProfiles = await _healthProfileService.GetAllHealthProfiles();
            return Ok(healthProfiles);
        }

        /// <summary>
        /// Retorna um perfil de saúde específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do perfil de saúde.</param>
        /// <returns>O DTO do perfil de saúde encontrado ou NotFound se não existir.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHealthProfileById(Guid id)
        {
            var healthProfile = await _healthProfileService.GetHealthProfileById(id);
            if (healthProfile == null)
                return NotFound();
            return Ok(healthProfile);
        }

        /// <summary>
        /// Cria um novo perfil de saúde.
        /// </summary>
        /// <param name="healthProfileDto">DTO do perfil de saúde a ser criado.</param>
        /// <returns>O DTO do perfil de saúde criado.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateHealthProfile([FromBody] HealthProfileDTO healthProfileDto)
        {
            var createdHealthProfile = await _healthProfileService.CreateHealthProfile(healthProfileDto);
            return CreatedAtAction(nameof(GetHealthProfileById), new { id = createdHealthProfile.Id }, createdHealthProfile);
        }

        /// <summary>
        /// Atualiza um perfil de saúde existente.
        /// </summary>
        /// <param name="id">O ID do perfil de saúde a ser atualizado.</param>
        /// <param name="healthProfileDto">DTO com dados atualizados do perfil de saúde.</param>
        /// <returns>NoContent se a atualização for bem-sucedida, NotFound se não encontrado.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHealthProfile(Guid id, [FromBody] HealthProfileDTO healthProfileDto)
        {
            if (id != healthProfileDto.Id)
                return BadRequest("ID mismatch");

            var existingHealthProfile = await _healthProfileService.GetHealthProfileById(id);
            if (existingHealthProfile == null)
                return NotFound();

            await _healthProfileService.UpdateHealthProfile(healthProfileDto);
            return NoContent();
        }

        /// <summary>
        /// Deleta um perfil de saúde pelo ID.
        /// </summary>
        /// <param name="id">O ID do perfil de saúde a ser deletado.</param>
        /// <returns>NoContent se deletado com sucesso, NotFound se não encontrado.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHealthProfile(Guid id)
        {
            var success = await _healthProfileService.DeleteHealthProfile(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
