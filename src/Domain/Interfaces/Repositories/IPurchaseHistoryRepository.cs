namespace Domain.Interfaces.Repositories;

public interface IPurchaseHistoryRepository : IRepository<PurchaseHistory>
{
    Task<IEnumerable<PurchaseHistory>> GetByUserId(Guid user);
}