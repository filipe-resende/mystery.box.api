namespace Infraestructure.Repository;

public class PurchaseHistoryRepository(Context dbContext) : Repository<PurchaseHistory>(dbContext), IPurchaseHistoryRepository
{
    public async Task<IEnumerable<PurchaseHistory>> GetByUserId(Guid userId)
    {
        return await dbContext.PurchaseHistory
            .Include(ph => ph.User)
            .Include(ph => ph.Payment)
            .Include(ph => ph.SteamCardCategory)
            .Where(ph => ph.UserId == userId)
            .OrderByDescending(ph => ph.Payment.CreatedAt)
            .ToListAsync();
    }
}