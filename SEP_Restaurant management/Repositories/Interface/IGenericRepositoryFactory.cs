namespace SEP_Restaurant_management.Repositories.Interface;

public interface IGenericRepositoryFactory
{
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}