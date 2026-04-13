using ERPFrigorifico.Application.Interfaces.Faenas;
using ERPFrigorifico.Shared.DTOs.Faenas;
using Microsoft.AspNetCore.Mvc;

namespace ERPFrigorifico.API.Controllers
{
    [ApiController]
    [Route("api/faenas")]
    public class FaenaController(IFaenaService faenaService) : ControllerBase
    {
        private readonly IFaenaService _faenaService = faenaService;

        [HttpPost("/enviar-animales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EnviarAnimalesAFaena([FromBody] List<int> animalIds)
        {
            //await _faenaService.EnviarAnimalesAFaena(animalIds);
            return Ok("Los animales han sido enviado a la faena exitosamente.");
        }

        [HttpPost("/procesar-faena")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ProcesarFaena([FromBody] ProcesarFaenaRequest request)
        {
            await _faenaService.ProcesarFaena(request.faenaId);
            return Ok("La faena ha comenzado con exito.");
        }
    }
}
