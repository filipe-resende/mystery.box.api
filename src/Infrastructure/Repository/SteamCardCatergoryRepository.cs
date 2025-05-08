namespace Infraestructure.Repository;

public class SteamCardCatergoryRepository(Context dbContext) : Repository<SteamCardCategory>(dbContext), ISteamCardCategoryRepository
{
}

