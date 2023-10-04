namespace Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity, TEntityDTO> : IDisposable
        where TEntity : class
    {
        Task<TEntity> Add(TEntityDTO entity);
        Task<TEntityDTO> GetById(Guid id);
        IEnumerable<TEntityDTO> GetAll();
        void Update(TEntityDTO entity);
        void Remove(int id);
    }
}
