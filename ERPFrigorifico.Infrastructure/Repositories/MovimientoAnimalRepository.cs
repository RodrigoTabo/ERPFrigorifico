using ERPFrigorifico.Application.Interfaces.MovimienosAnimal;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Infrastructure.Data;
using ERPFrigorifico.Shared.DTOs.MovimientosAnimales;
using ERPFrigorifico.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace ERPFrigorifico.Infrastructure.Repositories
{
    public class MovimientoAnimalRepository(ERPFrigorificoDbContext context) : IMovimientoAnimalRepository
    {

        private readonly ERPFrigorificoDbContext _context = context;

        public async Task<(List<MovimientoAnimal> Items, int TotalCount)> GetMovimientosPaginados(int pageIndex, int pageSize, TipoMovimiento? tipoMovimiento)
        {

            var query = _context.MovimientosAnimal
                .AsNoTracking()
                .Where(m => !tipoMovimiento.HasValue || m.TipoMovimiento == tipoMovimiento)
                                .Where(m => m.FechaMovimiento == _context.MovimientosAnimal
                    .Where(x => x.AnimalId == m.AnimalId)
                    .Max(x => x.FechaMovimiento));

            int totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(m => m.FechaMovimiento)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }


        // Este metodo obtiene una lista de animales cuyo ultimo movimiento coincide con el tipo de movimiento especificado.
        public async Task<List<int>> GetAnimalesPorUltimoMovimiento(List<int> animalSeleccionado, TipoMovimiento tipo)
            => await _context.MovimientosAnimal
                .Where(m => animalSeleccionado.Contains(m.AnimalId))
                .Where(m => m.FechaMovimiento == _context.MovimientosAnimal
                    .Where(x => x.AnimalId == m.AnimalId)
                    .Max(x => x.FechaMovimiento))
                .Where(m => m.TipoMovimiento == tipo)
                .Select(m => m.AnimalId)
                .ToListAsync();



        public async Task<List<MovimientoAnimalByIdResponse>> GetHistorialAnimalById(int id)
            => await _context.MovimientosAnimal.Select(m => new MovimientoAnimalByIdResponse
            { AnimalId = m.AnimalId, FechaMovimiento = m.FechaMovimiento, TipoMovimiento = m.TipoMovimiento }).Where(a => a.AnimalId == id).ToListAsync();

    }
}
