using ERPFrigorifico.Shared.DTOs.Ingresos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Application.Interfaces.Ingresos
{
    public interface IIngresoService
    {
        Task<int> RegistrarIngresoAsync(RegistrarIngresoRequest request);

        Task RegistrarSalidaAsync(string patente);

    }
}
