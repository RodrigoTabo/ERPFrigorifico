using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Shared.DTOs.Faenas;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Application.Interfaces.Faenas
{
    public interface IFaenaRepository
    {
        Task<Faena> GetFaenaById(int faenaId);
        Task<List<FaenaResponse>> GetAllFaenas();
        Task<FaenaResponse?> GetFaenaEnProceso();
        Task<Faena> FaenaActiva();

    }
}
