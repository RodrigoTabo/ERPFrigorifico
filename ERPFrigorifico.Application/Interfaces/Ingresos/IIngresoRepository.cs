using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Shared.DTOs.Ingresos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Application.Interfaces.Ingresos
{
    public interface IIngresoRepository
    {
        /// <summary>
        /// Guardo el ingreso
        /// </summary>
        void AddIngreso(Ingreso ingreso);

        /// <summary>
        /// Obtener objeto camion por ID
        /// </summary>
        Task<Camion?> ObtenerCamionAsync(int? id);

        /// <summary>
        /// Obtener objeto operario por ID
        /// </summary>
        Task<Operario?> ObtenerOperarioAsync(int? id);
        /// <summary>
        /// Obtenemos un ingreso activo por patente.
        /// </summary>
        Task<Ingreso> ObtenerIngresoActivoPorPatenteAsync(string patente);

        /// <summary>
        /// Obtener objeto proveedor por ID
        /// </summary>
        Task<Proveedor?> ObtenerProveedorAsync(int? id);
        /// <summary>
        /// Obtener lista de ingresos Activos
        /// </summary>
        Task<List<IngresoListadoResponse>> ListarIngresosActivos();
    }
}
