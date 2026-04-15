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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<FaenaResponse>>> GetAllFaenas()
            => Ok(await _faenaService.GetAllFaenas());

        [HttpGet("proceso")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<FaenaResponse>> GetFaenaEnProceso()
            => Ok(await _faenaService.GetFaenaEnProceso());

        //[HttpPost("/enviar-animales")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status409Conflict)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> EnviarAnimalesAFaena([FromBody] List<int> animalIds)
        //{
        //    await _faenaService.EnviarAnimalesAFaena(List<animalIds>);
        //    return Ok("Los animales han sido enviado a la faena exitosamente.");
        //}

        [HttpPost("{id}/procesar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ProcesarFaena(int id)
        {
            await _faenaService.ProcesarFaena(id);
            return Ok("La faena ha comenzado con exito.");
        }

        [HttpPost("{id}/terminar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> TerminarFaena(int id)
        {
            await _faenaService.TerminarFaena(id);
            return Ok("La faena ha comenzado con exito.");
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> TerminarFaena([FromBody] ProcesarFaenaRequest request)
        {
            await _faenaService.ProcesarFaena(request.faenaId);
            return Ok("La faena ha terminado con exito.");
        }
    }
}
