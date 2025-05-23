using Infrastructure.Data.DBContext;

namespace Infrastructure.Repository;

public class SteamCardCatergoryRepository(Context dbContext) : Repository<SteamCardCategory>(dbContext), ISteamCardCategoryRepository
{
}

