using ERPFrigorifico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Application.Interfaces.CamarasFrio
{
    public interface ICamaraFrioRepository
    {
        Task<CamaraFrio?> ObtenerCamaraById(int camaraFrioId);
    }
}
