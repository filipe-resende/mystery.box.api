namespace Application.Features.Handlers;

public class GetAuthenticationTokenHandler(
    IUserRepository repository,
    IMapper mapper,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor,
    ILogger<GetAuthenticationTokenHandler> logger
) : IRequestHandler<GetAuthenticationTokenCommand, Result>
{
    private readonly IUserRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    private readonly IConfiguration _config = config;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly ILogger<GetAuthenticationTokenHandler> _logger = logger;

    public async Task<Result> Handle(GetAuthenticationTokenCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Iniciando autenticação para o e-mail: {Email}", request.Email);

            var user = await _repository.GetUserByEmail(request.Email);

            if (user == null)
                return Result.Failure(new Error("AUTH001", "Usuário não encontrado."));

            else if (!string.IsNullOrWhiteSpace(request.Password))
            {
                if (string.IsNullOrWhiteSpace(user.Password) || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                    return Result.Failure(new Error("AUTH003", "Usuário ou senha inválidos."));
            }
            else
            {
                return Result.Failure(new Error("AUTH004", "Credenciais insuficientes."));
            }

            var keyBytes = Encoding.UTF8.GetBytes(_config["Jwt:Secret"]!);
            var securityKey = new SymmetricSecurityKey(keyBytes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString().ToLower()),
                    new Claim(ClaimTypes.Name, user.Name),
                ]),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            _httpContextAccessor.HttpContext?.Response.Cookies.Append("access_token", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddHours(2),
            });

            _logger.LogInformation("Token JWT gerado com sucesso para o usuário: {UserId}", user.Id);

            return Result.Success(new UserTokenDTO
            {
                User = _mapper.Map<UserDTO>(user),
                Token = tokenString
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao autenticar o usuário: {Email}", request.Email);
            return Result.Failure(new Error("AUTH999", $"Erro ao autenticar: {ex.Message}"));
        }
    }
}
