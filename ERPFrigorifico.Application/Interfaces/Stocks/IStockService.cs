namespace ERPFrigorifico.Application.Interfaces.Stocks
{
    public interface IStockService
    {
        Task GenerarStockCortes(int stockId);
    }
}
