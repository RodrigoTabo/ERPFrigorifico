using ERPFrigorifico.Application.Interfaces.Operarios;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Infrastructure.Data;
using ERPFrigorifico.Shared.DTOs.Operario;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Infrastructure.Repositories
{
    public class OperarioRepository(ERPFrigorificoDbContext context) : IOperarioRepository
    {
        private readonly ERPFrigorificoDbContext _context = context;

        public async Task<List<Operario>> GetAllOperariosHabilitados()
        {
            //Los traigo ya con el carnet vencidos.
            return await _context.Operarios
                .AsNoTracking()
                .Where(o => o.CarnetVencido == false)
                .OrderByDescending(o => o.DNI)
                .ToListAsync();
        }
    }
}
