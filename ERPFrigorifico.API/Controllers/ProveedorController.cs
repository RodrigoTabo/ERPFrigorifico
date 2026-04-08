using ERPFrigorifico.Application.Interfaces.Proveedores;
using ERPFrigorifico.Shared.DTOs.Proveedor;
using Microsoft.AspNetCore.Mvc;

namespace ERPFrigorifico.API.Controllers
{
    [ApiController]
    [Route("api/proveedores")]
    public class ProveedorController(IProveedorService proveedorService) : ControllerBase
    {
        private readonly IProveedorService _proveedorService = proveedorService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ProveedorResponse>>> GetAll()
            => Ok(await _proveedorService.ListarAsync());
    }
}
