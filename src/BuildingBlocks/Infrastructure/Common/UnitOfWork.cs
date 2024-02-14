using Contracts.Domains.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Entity.Validation;

namespace Infrastructure.Common;

public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
{
    private readonly TContext _context;
    private bool _disposed;
    private string _errorMessage = string.Empty;
    private IDbContextTransaction _transaction;

    public UnitOfWork(TContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task<int> CommitAsync()
    {
        //Commits the underlying store transaction
        try
        {
            await _transaction.CommitAsync();
            return 1;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    //public async Task RollbackAsync()
    //{
    //    _transaction.Rollback();
    //    await _transaction.DisposeAsync();
    //}

    //public async Task<int> SaveChangeAsync()
    //{
    //    try
    //    {
    //        return await _context.SaveChangesAsync();
    //    }
    //    catch (DbEntityValidationException dbEx)
    //    {
    //        foreach (var validationErrors in dbEx.EntityValidationErrors)
    //        {
    //            foreach (var validationError in validationErrors.ValidationErrors)
    //            {
    //                _errorMessage = _errorMessage + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage} {Environment.NewLine}";
    //            }
    //        }
    //        throw new Exception(_errorMessage, dbEx);
    //    }
    //}

    //Disposing of the Context Object
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _context.Dispose();
        _disposed = true;
    }

    //The Dispose() method is used to free unmanaged resources like files, 
    //database connections etc. at any time.
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}