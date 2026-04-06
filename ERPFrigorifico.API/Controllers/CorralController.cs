using ERPFrigorifico.Application.Interfaces.Corrales;
using ERPFrigorifico.Shared.DTOs.Corral;
using Microsoft.AspNetCore.Mvc;

namespace ERPFrigorifico.API.Controllers
{
    [ApiController]
    [Route("api/corrales")]
    public class CorralController(ICorralService corralService) : ControllerBase
    {
        private readonly ICorralService _corralService = corralService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EnviarAnimalesACorral([FromBody] EnviarAnimalesACorralRequest request)
        {
            await _corralService.EnviarAnimalesACorral(request.animalIds, request.corralId);
            return Ok("Los animales han sido enviado al corral exitosamente.");
        }
    }
}
