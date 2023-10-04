using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface ISteamCardRepository : IRepository<SteamCard, SteamCardDTO>
    {
    }
}
