using ERPFrigorifico.Application.Interfaces;
using ERPFrigorifico.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace ERPFrigorifico.Infrastructure.Repositories
{
    public class UnitOfWorkRepository(ERPFrigorificoDbContext context) : IUnitOfWorkRepository
    {
        private readonly ERPFrigorificoDbContext _context = context;
        private IDbContextTransaction? _transaction;

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
                await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
                await _transaction.RollbackAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
