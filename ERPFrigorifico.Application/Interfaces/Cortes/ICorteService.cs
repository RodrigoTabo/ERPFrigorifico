using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Application.Interfaces.Cortes
{
    public interface ICorteService
    {
        Task GenerarStockDesdeCorte(int corteId, int camaraFrioId);
    }
}
