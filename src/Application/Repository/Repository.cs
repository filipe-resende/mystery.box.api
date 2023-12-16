using Application.DBContext;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
    public abstract class Repository<TEntity, TEntityDTO> : IDisposable, IRepository<TEntity, TEntityDTO>
       where TEntity : class
    {
        protected Context dbContext;
        private readonly IMapper mapper;

        public Repository(Context dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<TEntity> Add(TEntityDTO entity)
        {
            var mappedEntity = mapper.Map<TEntity>(entity);
            await dbContext.Set<TEntity>().AddAsync(mappedEntity);
            await dbContext.SaveChangesAsync();
            return mappedEntity;
        }

        public async Task<TEntityDTO> GetById(Guid id)
        {
            var entity = await dbContext.Set<TEntity>().FindAsync(id);
            var mappedEntity = mapper.Map<TEntityDTO>(entity);
            dbContext.Set<TEntity>().Entry(entity).State = EntityState.Detached;
            return mappedEntity;
        }

        protected IQueryable<TEntity> All()
        {
            var entities = dbContext.Set<TEntity>().AsQueryable();
            return entities;
        }

        public IEnumerable<TEntityDTO> GetAll()
        {
            var entities = dbContext.Set<TEntity>().ToList();
            var mappedEntities = mapper.Map<List<TEntityDTO>>(entities);
            return mappedEntities;
        }

        public async Task Update(TEntityDTO entity)
        {
            var mappedEntity = mapper.Map<TEntity>(entity);
            dbContext.Set<TEntity>().Update(mappedEntity);
            await dbContext.SaveChangesAsync();
        }
        public void Remove(int id)
        {
            var entity = dbContext.Set<TEntity>().Find(id);
            var mappedEntity = mapper.Map<TEntity>(entity);
            dbContext.Set<TEntity>().Remove(mappedEntity);
            dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
