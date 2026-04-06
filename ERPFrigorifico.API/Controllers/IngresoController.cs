using ERPFrigorifico.Application.Interfaces.Ingresos;
using ERPFrigorifico.Shared.DTOs.Ingresos;
using Microsoft.AspNetCore.Mvc;

namespace ERPFrigorifico.API.Controllers
{
    [ApiController]
    [Route("api/Ingresos")]
    public class IngresoController(IIngresoService ingresoService) : ControllerBase
    {

        private readonly IIngresoService _ingresoService = ingresoService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegistrarIngreso([FromBody] RegistrarIngresoRequest request)
        {
            var id = await _ingresoService.RegistrarIngresoAsync(request);

            return Created($"api/Ingresos/{id}", request);
        }

        [HttpPost("salida")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegistrarSalida([FromQuery]string patente)
        {
            await _ingresoService.RegistrarSalidaAsync(patente);
            return Ok("La salida se ha registrado exitosamente.");
        }

    }
}