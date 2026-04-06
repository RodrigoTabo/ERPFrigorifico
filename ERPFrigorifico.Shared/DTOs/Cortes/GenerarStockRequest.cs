using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Shared.DTOs.Cortes
{
    public record GenerarStockRequest(int corteId, int camaraFrioId);
}
