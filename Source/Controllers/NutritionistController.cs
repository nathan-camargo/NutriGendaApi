using Microsoft.AspNetCore.Mvc;
using NutriGendaApi.src.Services;

namespace NutriGendaApi.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NutritionistController : ControllerBase
    {
        private readonly NutritionistService _nutritionistService;

        public NutritionistController(NutritionistService nutritionistService)
        {
            _nutritionistService = nutritionistService;
        }

        /// <summary>
        /// Retorna todos os nutricionistas cadastrados.
        /// </summary>
        /// <returns>Uma lista de nutricionistas.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllNutritionists()
        {
            var nutritionists = await _nutritionistService.GetAllNutritionists();
            return Ok(nutritionists);
        }

        /// <summary>
        /// Retorna um nutricionista específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do nutricionista.</param>
        /// <returns>O nutricionista encontrado ou NotFound se não existir.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNutritionistById(Guid id)
        {
            var nutritionist = await _nutritionistService.GetNutritionistById(id);
            if (nutritionist == null)
                return NotFound();
            return Ok(nutritionist);
        }

        /// <summary>
        /// Cria um novo nutricionista.
        /// </summary>
        /// <param name="nutritionist">Dados do nutricionista a ser criado.</param>
        /// <returns>O nutricionista criado.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateNutritionist([FromBody] Nutritionist nutritionist)
        {
            var createdNutritionist = await _nutritionistService.CreateNutritionist(nutritionist);
            return CreatedAtAction(nameof(GetNutritionistById), new { id = createdNutritionist.Id }, createdNutritionist);
        }

        /// <summary>
        /// Atualiza um nutricionista existente.
        /// </summary>
        /// <param name="id">O ID do nutricionista a ser atualizado.</param>
        /// <param name="nutritionist">Dados atualizados do nutricionista.</param>
        /// <returns>NoContent se a atualização for bem-sucedida, NotFound se não encontrado.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNutritionist(Guid id, [FromBody] Nutritionist nutritionist)
        {
            if (id != nutritionist.Id)
                return BadRequest("ID mismatch");

            var existingNutritionist = await _nutritionistService.GetNutritionistById(id);
            if (existingNutritionist == null)
                return NotFound();

            await _nutritionistService.UpdateNutritionist(nutritionist);
            return NoContent();
        }

        /// <summary>
        /// Deleta um nutricionista pelo ID.
        /// </summary>
        /// <param name="id">O ID do nutricionista a ser deletado.</param>
        /// <returns>NoContent se deletado com sucesso, NotFound se não encontrado.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNutritionist(Guid id)
        {
            var nutritionist = await _nutritionistService.GetNutritionistById(id);
            if (nutritionist == null)
                return NotFound();

            await _nutritionistService.DeleteNutritionist(id);
            return NoContent();
        }
    }
}
