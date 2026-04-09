using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Shared.DTOs.Operario;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Application.Interfaces.Operarios
{
    public interface IOperarioRepository
    {
        Task<List<Operario>> GetAllOperariosHabilitados();
    }
}
