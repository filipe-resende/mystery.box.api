namespace Infraestructure.Repository;

public abstract class Repository<TEntity>(Context dbContext) : IDisposable, IRepository<TEntity>
   where TEntity : class
{
    protected Context dbContext = dbContext;

    public async Task<TEntity> Add(TEntity entity)
    {
        await dbContext.Set<TEntity>().AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> GetById(Guid id)
    {
        return await dbContext.Set<TEntity>().FindAsync(id);
    }

    protected IQueryable<TEntity> All()
    {
        var entities = dbContext.Set<TEntity>().AsQueryable();
        return entities;
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
       return await dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task Update(TEntity entity)
    {
        try
        {
            dbContext.Set<TEntity>().Update(entity);
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {

            throw ex;
        }
       
    }
    public void Remove(int id)
    {
        var entity = dbContext.Set<TEntity>().Find(id);
        dbContext.Set<TEntity>().Remove(entity);
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

