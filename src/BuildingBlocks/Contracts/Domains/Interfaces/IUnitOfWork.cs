using Microsoft.EntityFrameworkCore;

namespace Contracts.Domains.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> CommitAsync();
    //Task BeginTransactionAsync();
    //Task CommitAsync();
    //Task RollbackAsync();
    //Task<int> SaveChangeAsync();
}

public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
}