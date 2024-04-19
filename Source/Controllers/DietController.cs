using Microsoft.AspNetCore.Mvc;
using NutriGendaApi.src.Services;

namespace NutriGendaApi.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DietController : ControllerBase
    {
        private readonly DietService _dietService;

        public DietController(DietService dietService)
        {
            _dietService = dietService;
        }

        /// <summary>
        /// Retorna todas as dietas cadastradas.
        /// </summary>
        /// <returns>Uma lista de dietas.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllDiets()
        {
            var diets = await _dietService.GetAllDiets();
            return Ok(diets);
        }

        /// <summary>
        /// Retorna uma dieta específica pelo ID.
        /// </summary>
        /// <param name="id">O ID da dieta.</param>
        /// <returns>A dieta correspondente ou NotFound se não encontrada.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDietById(Guid id)
        {
            var diet = await _dietService.GetDietById(id);
            if (diet == null)
                return NotFound();
            return Ok(diet);
        }

        /// <summary>
        /// Cria uma nova dieta.
        /// </summary>
        /// <param name="diet">O objeto Diet para criar.</param>
        /// <returns>A dieta criada.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateDiet([FromBody] Diet diet)
        {
            var createdDiet = await _dietService.CreateDiet(diet);
            return CreatedAtAction(nameof(GetDietById), new { id = createdDiet.Id }, createdDiet);
        }

        /// <summary>
        /// Atualiza uma dieta existente.
        /// </summary>
        /// <param name="id">O ID da dieta a ser atualizada.</param>
        /// <param name="diet">Os novos dados para a dieta.</param>
        /// <returns>NoContent se a atualização foi bem-sucedida, NotFound se a dieta não foi encontrada.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiet(Guid id, [FromBody] Diet diet)
        {
            if (id != diet.Id)
                return BadRequest("ID mismatch");

            var existingDiet = await _dietService.GetDietById(id);
            if (existingDiet == null)
                return NotFound();

            await _dietService.UpdateDiet(diet);
            return NoContent();
        }

        /// <summary>
        /// Deleta uma dieta.
        /// </summary>
        /// <param name="id">O ID da dieta a ser deletada.</param>
        /// <returns>NoContent se deletado com sucesso, NotFound se não encontrada.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiet(Guid id)
        {
            var diet = await _dietService.GetDietById(id);
            if (diet == null)
                return NotFound();

            await _dietService.DeleteDiet(id);
            return NoContent();
        }

        /// <summary>
        /// Retorna as dietas de um usuário específico.
        /// </summary>
        /// <param name="userId">O ID do usuário.</param>
        /// <returns>Uma lista de dietas do usuário.</returns>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetDietsByUserId(Guid userId)
        {
            var diets = await _dietService.GetDietsByUserId(userId);
            return Ok(diets);
        }
    }
}
