using Application.DBContext;
using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Repository
{
    public class SteamCardRepository : Repository<SteamCard, SteamCardDTO>, ISteamCardRepository
    {
        public SteamCardRepository(Context dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
