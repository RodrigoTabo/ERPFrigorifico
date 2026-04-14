using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Shared.DTOs.MovimientosAnimales;
using ERPFrigorifico.Shared.Enums;

namespace ERPFrigorifico.Application.Interfaces.MovimienosAnimal
{
    public interface IMovimientoAnimalRepository
    {
        Task<(List<MovimientoAnimal> Items, int TotalCount)> GetMovimientosPaginados(int pageIndex, int pageSize, TipoMovimiento? tipoMovimiento);
        Task<List<Animal>> GetAnimalesPorUltimoMovimiento(List<int> AnimalIds, TipoMovimiento TipoMovimiento);
        Task<List<MovimientoAnimalByIdResponse>> GetHistorialAnimalById(int id);
    }
}
