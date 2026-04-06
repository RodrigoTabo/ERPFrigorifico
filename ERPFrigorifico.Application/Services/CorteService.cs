using ERPFrigorifico.Application.Exceptions;
using ERPFrigorifico.Application.Interfaces;
using ERPFrigorifico.Application.Interfaces.CamarasFrio;
using ERPFrigorifico.Application.Interfaces.Cortes;
using ERPFrigorifico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Application.Services
{
    public class CorteService(ICorteRepository corteRepository, ICamaraFrioRepository camaraFrioRepository, IUnitOfWorkRepository unitOfWorkRepository) : ICorteService
    {

        private readonly ICorteRepository _corteRepository = corteRepository;
        private readonly ICamaraFrioRepository _camaraFrioRepository = camaraFrioRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

        public async Task GenerarStockDesdeCorte(int corteId, int camaraFrioId)
        {
            var corte = await _corteRepository.GetCorteById(corteId);

            ExisteCorte(corte);

            CorteYaTieneStock(corte);

            var camaraExiste = await _camaraFrioRepository.ObtenerCamaraById(camaraFrioId);

            CamaraFrioExiste(camaraExiste);

            var stock = new Stock
            {
                CorteId = corteId,
                CamaraFrioId = camaraFrioId,
                Peso = corte.Peso
            };

            corte.Stocks.Add(stock);

            await _unitOfWorkRepository.SaveChangesAsync();

        }

        private void ExisteCorte(Corte corte)
        {
            if (corte == null)
                throw new NotFoundException("El corte no existe.");
        }
        private void CorteYaTieneStock(Corte corte)
        {
            if (corte.Stocks.Any())
                throw new ConflictException("El corte ya tiene stock generado.");
        }

        private void CamaraFrioExiste(CamaraFrio? camaraFrio)
        {
            if (camaraFrio == null)
                throw new NotFoundException("La cámara de frío no existe.");
        }


    }
}
