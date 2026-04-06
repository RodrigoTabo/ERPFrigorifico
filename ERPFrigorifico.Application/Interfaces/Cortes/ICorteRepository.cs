using ERPFrigorifico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Application.Interfaces.Cortes
{
    public interface ICorteRepository
    {
        Task<Corte> GetCorteById(int corteId);
    }
}
