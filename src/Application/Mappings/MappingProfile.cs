namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<SteamCard, SteamCardDTO>().ReverseMap();
        CreateMap<SteamCardCategory, SteamCardCategoryDTO>().ReverseMap();
    }
}

