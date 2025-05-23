using Infrastructure.Data.DBContext;

namespace Infrastructure.Repository;

public class SteamCardRepository(Context dbContext) : Repository<SteamCard>(dbContext), ISteamCardRepository
{
    public async Task<IEnumerable<SteamCard>> GetSteamCards(IEnumerable<CartItem> items)
    {
        var result = new List<SteamCard>();

        foreach (var item in items)
        {
            var cards = await dbContext.SteamCards
                .Where(card =>
                    card.SteamCardCategoryId == item.Id &&
                    (card.Status == SteamCardStatus.Available ||
                     card.Status == SteamCardStatus.Reserved) &&
                     card.Active)
                .OrderBy(card => card.CreatedAt)
                .Take(item.Quantity)
                .ToListAsync();

            result.AddRange(cards);
        }

        return result;
    }
}

