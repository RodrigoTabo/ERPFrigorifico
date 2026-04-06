using ERPFrigorifico.Application.Exceptions;
using ERPFrigorifico.Application.Interfaces.Stocks;

namespace ERPFrigorifico.Application.Services
{
    public class StockService(IStockRepository stockRepository) : IStockService
    {

        private readonly IStockRepository _stockRepository = stockRepository;

        public async Task GenerarStockCortes(int stockId)
        {
            var stock = await _stockRepository.GetStockById(stockId);

            if (stock == null)
                throw new NotFoundException("El Stock no existe.");
            //Documente esto porque estaba haciendo cualquiera, igual dejo todo esto creado porque voy a necesitarlo para cuando agregue
            //la logistica del ERP para poder crear toda la logica y bla bla bla...
            //if(stock.CamaraFrio.Any())
            //    throw new BadRequestException("El Stock ya tiene cámaras de frío asociadas.");

        }
    }
}
