using ERPFrigorifico.Application.Interfaces.MovimienosAnimal;
using ERPFrigorifico.Shared.DTOs;
using ERPFrigorifico.Shared.DTOs.MovimientosAnimales;
using ERPFrigorifico.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ERPFrigorifico.API.Controllers
{
    [ApiController]
    [Route("api/movimientosanimales")]
    public class MovimientoAnimalController(IMovimientoAnimalService movimientoAnimalService) : ControllerBase
    {

        private readonly IMovimientoAnimalService _movimientoAnimalService = movimientoAnimalService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<MovimientoAnimalResponse>>> GetAllMovimientosAnimales(int pageIndex, int pageSize, TipoMovimiento? tipoMovimiento)
            => Ok(await _movimientoAnimalService.GetAllMovimientosAnimales(pageIndex, pageSize, tipoMovimiento));

        [HttpPost]
        public async Task<ActionResult> EnviarAnimales(List<int> animalIds)
            => Ok(_movimientoAnimalService.EnviarAnimales(animalIds));
    }
}
