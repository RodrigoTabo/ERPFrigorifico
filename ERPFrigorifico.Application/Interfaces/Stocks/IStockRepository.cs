using ERPFrigorifico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Application.Interfaces.Stocks
{
    public interface IStockRepository
    {
        Task <Stock> GetStockById(int stockId);
    }
}
