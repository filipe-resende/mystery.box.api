namespace Application.Features.Handlers;

public class GetAuthenticationTokenHandler(IUserRepository repository, IAuthenticationService authenticationService, IMapper mapper) : IRequestHandler<GetAuthenticationTokenCommand, UserTokenDTO>
{
    private readonly IUserRepository _repository = repository;
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly IMapper _mapper = mapper;

    public async Task<UserTokenDTO> Handle(GetAuthenticationTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByLogin(request.Email, request.Password);

        if (user == null)
            throw new Exception("User Not Found");

        var token = _authenticationService.CreateAuthenticationToken(user);

        return new UserTokenDTO
        {
            User = _mapper.Map<UserDTO>(user),
            Token = token
        };
    }
}

