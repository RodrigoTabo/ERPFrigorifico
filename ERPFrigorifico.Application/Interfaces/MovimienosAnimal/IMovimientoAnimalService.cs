using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Shared.DTOs;
using ERPFrigorifico.Shared.DTOs.MovimientosAnimales;
using ERPFrigorifico.Shared.Enums;


namespace ERPFrigorifico.Application.Interfaces.MovimienosAnimal
{
    public interface IMovimientoAnimalService
    {
        Task<PagedResult<MovimientoAnimalResponse>> GetAllMovimientosAnimales(int pageIndex, int pageSize, TipoMovimiento? tipoMovimiento);
        Task EnviarAnimales(List<int> animalIds);
    }
}
