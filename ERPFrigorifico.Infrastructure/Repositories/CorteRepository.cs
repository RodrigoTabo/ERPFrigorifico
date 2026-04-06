using ERPFrigorifico.Application.Interfaces.Cortes;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Infrastructure.Repositories
{
    public class CorteRepository(ERPFrigorificoDbContext context) : ICorteRepository
    {

        private readonly ERPFrigorificoDbContext _context = context;

        public async Task<Corte> GetCorteById(int corteId)
        {
            var corte = await _context.Cortes
                .Include(s => s.Stocks)
                .FirstOrDefaultAsync(c => c.Id == corteId);

            return corte;

        }
    }
}
