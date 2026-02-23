using Microsoft.EntityFrameworkCore;

namespace SEP_Restaurant_management.Repositories.Interface;

public interface IUnitOfWork : IGenericRepositoryFactory, IDisposable
{
    int Commit();
    
    Task<int> CommitAsync();

    void Rollback();
    
    Task RollbackAsync();
    
    void BeginTransaction();
    
    Task BeginTransactionAsync();
}

public interface IUnitOfWork<TContext> : IUnitOfWork
{
    TContext DbContext { get; }
}


