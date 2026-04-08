using ERPFrigorifico.Application.Interfaces.Ingresos;
using ERPFrigorifico.Shared.DTOs.Ingresos;
using Microsoft.AspNetCore.Mvc;

namespace ERPFrigorifico.API.Controllers
{
    [ApiController]
    [Route("api/ingresos")]
    public class IngresoController(IIngresoService ingresoService) : ControllerBase
    {

        private readonly IIngresoService _ingresoService = ingresoService;

        [HttpPost("ingreso")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegistrarIngreso([FromBody] RegistrarIngresoRequest request)
        {
            var id = await _ingresoService.RegistrarIngresoAsync(request);

            return Created($"api/Ingresos/{id}", request);
        }

        [HttpDelete("salida/{patente}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegistrarSalida(string patente)
        {
            await _ingresoService.RegistrarSalidaAsync(patente);
            return Ok("La salida se ha registrado exitosamente.");
        }

        [HttpGet("ingresosactivos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<IngresoListadoResponse>>> GetIngresosActivos()
            => Ok(await _ingresoService.ListarIngresosActivos());

    }
}