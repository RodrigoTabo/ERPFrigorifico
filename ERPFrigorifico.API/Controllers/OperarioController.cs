using ERPFrigorifico.Application.Interfaces.Operarios;
using ERPFrigorifico.Shared.DTOs.Operario;
using Microsoft.AspNetCore.Mvc;

namespace ERPFrigorifico.API.Controllers
{
    [ApiController]
    [Route("api/operarios")]
    public class OperarioController(IOperarioService operarioService) : ControllerBase
    {

        private readonly IOperarioService _operarioService = operarioService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<OperarioResponse>>> GetAllOperariosHabilitados()
            => Ok(await _operarioService.ListarOperarios());
    }
}
