using ERPFrigorifico.Application.Interfaces.CamarasFrio;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Infrastructure.Repositories
{
    public class CamaraFrioRepository(ERPFrigorificoDbContext context) : ICamaraFrioRepository
    {
        private readonly ERPFrigorificoDbContext _context = context;

        public async Task<CamaraFrio?> ObtenerCamaraById(int camaraFrioId)
        {
            var camaraFrio = await _context.CamarasFrio.FindAsync(camaraFrioId);

            return camaraFrio;
        }
    }
}
