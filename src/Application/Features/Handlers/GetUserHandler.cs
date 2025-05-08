namespace Application.Features.Handlers;

public class GetUserHandler(IUserRepository repository, IMapper mapper) : IRequestHandler<GetUserCommand, UserDTO>
{
    private readonly IUserRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<UserDTO> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _repository.GetById(request.Id);
            return _mapper.Map<UserDTO>(user);
        }
        catch (Exception ex)
        {
            throw new Exception($"could not be saved. Error {ex.Message}");
        }
    }
}