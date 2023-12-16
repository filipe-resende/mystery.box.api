using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface ISteamCardCategoryRepository : IRepository<SteamCardCategory, SteamCardCategoryDTO>
    {
    }
}
