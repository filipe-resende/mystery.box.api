namespace Application.Features.Handlers;

public class GetSteamCardHandler(ISteamCardRepository repository, IMapper mapper) : IRequestHandler<GetSteamCardCommand, SteamCardDTO>
{
    private readonly ISteamCardRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<SteamCardDTO> Handle(GetSteamCardCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var steamcard = await _repository.GetById(request.Id);
            return _mapper.Map<SteamCardDTO>(steamcard);
        }
        catch (Exception ex)
        {
            throw new Exception($"could not be saved. Error {ex.Message}");
        }
    }
}

