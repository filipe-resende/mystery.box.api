namespace Infraestructure.Repository;

public class SteamCardRepository(Context dbContext) : Repository<SteamCard>(dbContext), ISteamCardRepository
{
}

