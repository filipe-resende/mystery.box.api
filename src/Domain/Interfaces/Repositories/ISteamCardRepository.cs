namespace Domain.Interfaces.Repositories;

public interface ISteamCardRepository : IRepository<SteamCard>
{
    Task<IEnumerable<SteamCard>> GetSteamCards(IEnumerable<CartItem> items);
}

