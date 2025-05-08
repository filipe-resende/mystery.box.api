namespace Application.Features.Handlers;

public class GetAllSteamCardHandler(ISteamCardCategoryRepository repository, IMapper mapper) : IRequestHandler<GetAllSteamCardCommand, IEnumerable<SteamCardCategoryDTO>>
{
    private readonly ISteamCardCategoryRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<SteamCardCategoryDTO>> Handle(GetAllSteamCardCommand request, CancellationToken cancellationToken)
    {
        var category =  await _repository.GetAll();
        return _mapper.Map<IEnumerable<SteamCardCategoryDTO>>(category);
    }
}

