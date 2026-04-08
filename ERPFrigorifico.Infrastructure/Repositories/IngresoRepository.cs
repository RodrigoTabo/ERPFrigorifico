using ERPFrigorifico.Application.Interfaces.Ingresos;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Infrastructure.Data;
using ERPFrigorifico.Shared.DTOs.Ingresos;
using Microsoft.EntityFrameworkCore;

namespace ERPFrigorifico.Infrastructure.Repositories
{
    public class IngresoRepository(ERPFrigorificoDbContext context) : IIngresoRepository
    {
        private readonly ERPFrigorificoDbContext _context = context;

        public void AddIngreso(Ingreso ingreso)
        {
            _context.Ingresos.Add(ingreso);
        }

        public async Task<Camion?> ObtenerCamionAsync(int? id)
        {
            //Traigo el objeto completo para realizar validaciones
            var camion = await _context.Camiones.FindAsync(id);
            //Lo retormanos al servicio.
            return camion;
        }

        public async Task<Operario?> ObtenerOperarioAsync(int? id)
        {
            //Traigo el objeto completo para realizar validaciones
            var operario = await _context.Operarios.FindAsync(id);
            //Lo retormanos al servicio.
            return operario;
        }

        public async Task<Ingreso?> ObtenerIngresoActivoPorPatenteAsync(string patente)
        {
            //Traigo el objeto completo para realizar validaciones
            var ingreso = await _context.Ingresos.FirstOrDefaultAsync(i => i.Patente == patente && i.FechaSalida == null);
            //Lo retormanos al servicio.
            return ingreso;
        }

        public async Task<Proveedor?> ObtenerProveedorAsync(int? id)
        {
            //Traigo el objeto completo para realizar validaciones
            var proveedor = await _context.Proveedores.FindAsync(id);
            //Lo retormanos al servicio.
            return proveedor;
        }

        public async Task<List<IngresoListadoResponse>> ListarIngresosActivos()
        {
            //Query hecha en SQLServer y adaptada a LINQ.
            var query = from i in _context.Ingresos
                        join p in _context.Proveedores on i.ProveedorId equals p.Id into provJoin
                        from p in provJoin.DefaultIfEmpty()
                        join o in _context.Operarios on i.OperarioId equals o.Id into opJoin
                        from o in opJoin.DefaultIfEmpty()
                        where i.FechaSalida == null
                        orderby i.FechaIngreso descending
                        select new IngresoListadoResponse
                        {
                            Nombre = p != null ? p.Nombre : o.Nombre,
                            Patente = i.Patente,
                            FechaIngreso = i.FechaIngreso,
                            MinutosEnPlanta = EF.Functions.DateDiffMinute(i.FechaIngreso, DateTime.Now),
                            TipoIngreso = i.TipoIngreso,
                        };

            return await query.ToListAsync();
        }
    }
}
