namespace Domain.Interfaces.Repositories;

public interface IRepository<TEntity> : IDisposable
    where TEntity : class
{
    Task<TEntity> Add(TEntity entity);
    Task<TEntity> GetById(Guid id);
    Task<IEnumerable<TEntity>> GetAll();
    Task Update(TEntity entity);
    void Remove(int id);
}