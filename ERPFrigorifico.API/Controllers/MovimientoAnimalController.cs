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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<MovimientoAnimalByIdResponse>>> GetHistorialAnimalById(int id)
            => Ok(await _movimientoAnimalService.GetHistorialAnimalById(id));


        [HttpPost]
        public async Task<IActionResult> EnviarAnimales(List<int> animalIds)
        {
            await _movimientoAnimalService.EnviarAnimales(animalIds);
            return Ok();
        }
    }
}
