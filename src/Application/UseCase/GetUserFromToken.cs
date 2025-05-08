namespace Application.UseCase;

public class GetUserFromToken(IHttpContextAccessor contextAccessor, IUserRepository _repository, IMapper mapper) : IGetUserFromToken
{
    private readonly IHttpContextAccessor contextAccessor = contextAccessor;
    private readonly IUserRepository _repository = _repository;
    private readonly IMapper _mapper = mapper;

    public async Task<UserDTO> GetUserIdFromToken()
    {
        var sid =
            contextAccessor.
            HttpContext?.
            User?.
            Claims?.
            FirstOrDefault(c => c.Type == ClaimTypes.Sid)?
            .Value;

        if (sid != null)
        {
            var id = Guid.Parse(sid);
            var user = await _repository.GetById(id);

            return _mapper.Map<UserDTO>(user);
        }
        else
        {
            return null;
        }
    }
}

