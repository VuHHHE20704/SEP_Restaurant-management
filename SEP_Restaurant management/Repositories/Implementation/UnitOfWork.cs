using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SEP_Restaurant_management.Repositories.Implementation;
using SEP_Restaurant_management.Repositories.Interface;

namespace SEP_Restaurant_management.Repositories.Implementation;

public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
{
    private readonly TContext _dbContext;
    private bool _disposed;
    private Dictionary<Type, object> _repositories;
    private IDbContextTransaction? _transaction;
    
    public TContext DbContext => _dbContext;
    
    public UnitOfWork(TContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _repositories = new Dictionary<Type, object>();
    }
    
    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity);

        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new GenericRepository<TEntity>(_dbContext);
        }

        return (IGenericRepository<TEntity>)_repositories[type];
    }
    
    public int Commit()
    {
        return _dbContext.SaveChanges();
    }
    
    public async Task<int> CommitAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
    
    public void BeginTransaction()
    {
        _transaction = _dbContext.Database.BeginTransaction();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync();
    }
    
    public void Rollback()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
        _transaction = null;
    }
    
    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _transaction?.Dispose();
                _dbContext?.Dispose();

                foreach (var repository in _repositories.Values)
                {
                    if (repository is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }

            // Clear repositories
            _repositories?.Clear();
            _transaction = null;
            _disposed = true;
        }
    }
}
