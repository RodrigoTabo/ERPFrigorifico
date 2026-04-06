using ERPFrigorifico.Application.Interfaces.Canales;
using ERPFrigorifico.Shared.DTOs.Canales;
using Microsoft.AspNetCore.Mvc;

namespace ERPFrigorifico.API.Controllers
{
    [ApiController]
    [Route("api/Canales")]
    public class CanalController(ICanalService canalService) : ControllerBase
    {

        private readonly ICanalService _canalService = canalService;

        [HttpPost("/media-reses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GenerarMediaReses([FromBody]GenerarMediaResesRequest request)
        {
            await _canalService.GenerarMediaReses(request.canalId);
            return Ok("Media reses generada con exito.");
        }

    }
}
