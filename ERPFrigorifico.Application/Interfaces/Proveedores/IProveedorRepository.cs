using ERPFrigorifico.Domain.Entities;

namespace ERPFrigorifico.Application.Interfaces.Proveedores
{
    public interface IProveedorRepository
    {
        Task<List<Proveedor>> GetAll();
    }
}
