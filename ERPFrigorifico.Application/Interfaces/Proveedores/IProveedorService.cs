using ERPFrigorifico.Shared.DTOs.Proveedor;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Application.Interfaces.Proveedores
{
    public interface IProveedorService
    {
        Task<List<ProveedorResponse>> ListarAsync();
    }
}
