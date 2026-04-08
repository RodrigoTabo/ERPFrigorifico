using ERPFrigorifico.Application.Interfaces.Proveedores;
using ERPFrigorifico.Shared.DTOs.Proveedor;

namespace ERPFrigorifico.Application.Services
{
    public class ProveedorService(IProveedorRepository proveedorRepository) : IProveedorService
    {

        private readonly IProveedorRepository _proveedorRepository = proveedorRepository;

        public async Task<List<ProveedorResponse>> ListarAsync()
        {
            var query = await _proveedorRepository.GetAll();

            var consultaFinal = query
                .OrderByDescending(q => q.CUIL)
                .Select(p => new ProveedorResponse
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Cuil = p.CUIL,
                    Direccion = p.Direccion,
                }).ToList();


            return consultaFinal;

        }
    }
}
