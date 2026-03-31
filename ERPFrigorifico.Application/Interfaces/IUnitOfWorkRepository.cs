using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFrigorifico.Application.Interfaces
{
    public interface IUnitOfWorkRepository
    {
        Task SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
