using ERPFrigorifico.Application.Interfaces.Animales;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPFrigorifico.Infrastructure.Repositories
{
    public class AnimalRepository(ERPFrigorificoDbContext context) : IAnimalRepository
    {

        private readonly ERPFrigorificoDbContext _context = context;

        public async Task<List<Animal>> GetByIds(List<int> animalIds)
        {
            return await _context.Animales
                .Where(a => animalIds.Contains(a.Id))
                .ToListAsync();
        }
    }
}
