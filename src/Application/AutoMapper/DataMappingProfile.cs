using AutoMapper;
using Domain.DTO;
using Domain.Entities;

namespace Application.AutoMapper
{
    public class DataMappingProfile : Profile
    {
        public DataMappingProfile()
        {
            CreateMap<SteamCard, SteamCardDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<SteamCardCategory, SteamCardCategoryDTO>().ReverseMap();

        }
    }
}
