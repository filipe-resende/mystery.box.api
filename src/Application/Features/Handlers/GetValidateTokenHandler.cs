namespace Application.Features.Handlers;

public class GetValidateTokenHandler(IUserRepository repository, IAuthenticationService authenticationService, IMapper mapper) : IRequestHandler<GetValidateTokenCommand, UserTokenDTO>
{
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly IUserRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<UserTokenDTO> Handle(GetValidateTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetById(request.UserId);
        var token = _authenticationService.CreateAuthenticationToken(user);

        return new UserTokenDTO
        {
            User = _mapper.Map<UserDTO>(user),
            Token = token
        };
    }
}

