using ERPFrigorifico.Application.Interfaces.Faenas;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Infrastructure.Repositories
{
    public class FaenaRepository(ERPFrigorificoDbContext context) : IFaenaRepository
    {
        private readonly ERPFrigorificoDbContext _context = context;
        public async Task<Faena> GetFaenaById(int faenaId)
        {
            var faena = _context.Faenas
                .Include(f => f.Animales)
                .FirstOrDefault(f => f.Id == faenaId);

            return faena;
        }
    }
}
