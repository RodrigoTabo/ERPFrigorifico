using ERPFrigorifico.Application.Interfaces.MovimienosAnimal;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Domain.Enums;
using ERPFrigorifico.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPFrigorifico.Infrastructure.Repositories
{
    public class MovimientoAnimalRepository(ERPFrigorificoDbContext context) : IMovimientoAnimalRepository
    {

        private readonly ERPFrigorificoDbContext _context = context;


        // Este metodo obtiene una lista de animales cuyo ultimo movimiento coincide con el tipo de movimiento especificado.
        public async Task<List<Animal>> GetAnimalesPorUltimoMovimiento(TipoMovimiento tipo)
        {
            var animalesIds = await _context.MovimientosAnimal
                .Where(m => m.FechaMovimiento == _context.MovimientosAnimal
                    .Where(x => x.AnimalId == m.AnimalId)
                    .Max(x => x.FechaMovimiento))
                .Where(m => m.TipoMovimiento == tipo)
                .Select(m => m.AnimalId)
                .ToListAsync();

            return await _context.Animales
                .Where(a => animalesIds.Contains(a.Id))
                .ToListAsync();
        }
    }
}
