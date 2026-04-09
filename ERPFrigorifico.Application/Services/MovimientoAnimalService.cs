
using ERPFrigorifico.Application.Interfaces.MovimienosAnimal;
using ERPFrigorifico.Shared.DTOs;
using ERPFrigorifico.Shared.DTOs.MovimientosAnimales;
using ERPFrigorifico.Shared.Enums;

namespace ERPFrigorifico.Application.Services
{
    public class MovimientoAnimalService(IMovimientoAnimalRepository movimientoAnimalRepository) : IMovimientoAnimalService
    {

        private readonly IMovimientoAnimalRepository _movimientoAnimalRepository = movimientoAnimalRepository;
        public async Task<PagedResult<MovimientoAnimalResponse>> GetAllMovimientosAnimales(int pageIndex, int pageSize, TipoMovimiento? tipoMovimiento)
        {
            var query = await _movimientoAnimalRepository.GetMovimientosPaginados(pageIndex, pageSize, tipoMovimiento);

            var ultimaConsulta = query.Items
                .Select(m => new MovimientoAnimalResponse
                {
                    Id = m.Id,
                    CorralId = m.CorralId,
                    FaenaId = m.FaenaId,
                    FechaMovimiento = m.FechaMovimiento,
                    tipoMovimiento = m.TipoMovimiento,
                }).ToList();

            return new PagedResult<MovimientoAnimalResponse>
            {
                Items = ultimaConsulta,
                TotalCount = query.TotalCount
            };
        }
    }
}
