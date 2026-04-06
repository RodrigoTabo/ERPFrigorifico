using ERPFrigorifico.Application.Interfaces.Stocks;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Infrastructure.Repositories
{
    public class StockRepository(ERPFrigorificoDbContext context) : IStockRepository
    {

        private readonly ERPFrigorificoDbContext _context = context;

        public async Task<Stock> GetStockById(int stockId)
        {
            var stock = await _context.Stocks
                .Include(s => s.Corte)
                .Include(c => c.CamaraFrio)
                .FirstOrDefaultAsync(s => s.Id == stockId);

            return stock;
        }
    }
}
