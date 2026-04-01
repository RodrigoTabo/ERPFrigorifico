using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Domain.Enums;
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
