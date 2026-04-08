using ERPFrigorifico.Application.Interfaces.Proveedores;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPFrigorifico.Infrastructure.Repositories
{
    public class ProveedorRepository(ERPFrigorificoDbContext context) : IProveedorRepository
    {

        private readonly ERPFrigorificoDbContext _context = context;

        public async Task<List<Proveedor>> GetAll()
        => await _context.Proveedores.AsNoTracking().OrderByDescending(p => p.CUIL).ToListAsync();

    }
}
