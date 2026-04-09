using ERPFrigorifico.Shared.DTOs.Operario;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Application.Interfaces.Operarios
{
    public interface IOperarioService
    {
        Task<List<OperarioResponse>> ListarOperarios();
    }
}
