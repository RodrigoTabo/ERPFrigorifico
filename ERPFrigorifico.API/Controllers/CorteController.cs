using ERPFrigorifico.Application.Interfaces.Cortes;
using ERPFrigorifico.Shared.DTOs.Cortes;
using Microsoft.AspNetCore.Mvc;

namespace ERPFrigorifico.API.Controllers
{
    [ApiController]
    [Route("api/cortes")]
    public class CorteController(ICorteService corteService) : ControllerBase
    {

        private readonly ICorteService _corteService = corteService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GenerarStockDesdeCorte([FromBody] GenerarStockRequest request)
        {
            await _corteService.GenerarStockDesdeCorte(request.corteId, request.camaraFrioId);
            return Ok("Stock generado exitosamente desde el corte.");
        }
    }
}
