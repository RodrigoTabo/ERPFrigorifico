using ERPFrigorifico.Application.Interfaces.Canales;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace ERPFrigorifico.Infrastructure.Repositories
{
    public class CanalRepository(ERPFrigorificoDbContext context) : ICanalRepository
    {

        private readonly ERPFrigorificoDbContext _context = context;

        public async Task<Canal> GetCanalById(int canalId)
        {
            var canal = await _context.Canales
                .Include(c => c.Cortes)
                .FirstOrDefaultAsync(c => c.Id == canalId);

            return canal;
        }
    }
}
