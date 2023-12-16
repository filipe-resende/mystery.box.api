using Application.DBContext;
using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Repository
{
    public class SteamCardCatergoryRepository : Repository<SteamCardCategory, SteamCardCategoryDTO>, ISteamCardCategoryRepository
    {
        public SteamCardCatergoryRepository(Context dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
