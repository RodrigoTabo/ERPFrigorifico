using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Application.Interfaces.MovimienosAnimal
{
    public interface IMovimientoAnimalRepository
    {
        Task<List<Animal>> GetAnimalesPorUltimoMovimiento(TipoMovimiento tipo);
    }
}
