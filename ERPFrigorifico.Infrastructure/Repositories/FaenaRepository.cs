using ERPFrigorifico.Application.Interfaces.Faenas;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Infrastructure.Data;
using ERPFrigorifico.Shared.DTOs.Faenas;
using ERPFrigorifico.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Infrastructure.Repositories
{
    public class FaenaRepository(ERPFrigorificoDbContext context) : IFaenaRepository
    {
        private readonly ERPFrigorificoDbContext _context = context;

        public async Task<Faena> FaenaActiva()
        {
            var faena = await _context.Faenas.Where(f => f.TipoFaena == TipoFaena.Pendiente || f.TipoFaena == TipoFaena.EnCurso).FirstOrDefaultAsync();

            if (faena is not null)
                return faena;

            var nueva = new Faena
            {
                TipoFaena = TipoFaena.Pendiente,
                FechaProduccion = DateTime.UtcNow
            };

            _context.Faenas.Add(nueva);

            return nueva;
        }

        public Task<List<FaenaResponse>> GetAllFaenas()
        {
            var lista = _context.Faenas.AsNoTracking();

            //TODO: Filtrar por cantidades para paginado.
            //Lo filtramos ya ordenado.
            return lista.Select(f => new FaenaResponse
            {
                FechaProduccion = f.FechaProduccion,
                TipoFaena = f.TipoFaena,
            }).OrderByDescending(f => f.FechaProduccion).ToListAsync();
        }

        public async Task<Faena> GetFaenaById(int faenaId)
        {
            var faena = _context.Faenas
                .Include(f => f.Animales)
                .Include(c => c.Canales)
                .Where(f => f.TipoFaena != TipoFaena.Procesada)
                .FirstOrDefault(f => f.Id == faenaId);

            return faena;
        }

        public async Task<FaenaResponse?> GetFaenaEnProceso()
        {
            return await _context.Faenas
                .Select(f => new FaenaResponse
                {
                    Id = f.Id,
                    FechaProduccion = f.FechaProduccion,
                    TipoFaena = f.TipoFaena,
                })
                .OrderByDescending(f => f.FechaProduccion)
                .Where(f => f.TipoFaena != TipoFaena.Procesada)
                .FirstAsync();
        }
    }
}
