namespace Application.Services;

public class AuthenticationService(IConfiguration _configuration) : IAuthenticationService
{
    private readonly IConfiguration _configuration = _configuration;

    public string CreateAuthenticationToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        string jwtSecret = _configuration["Jwt:Secret"]!;

        if (string.IsNullOrEmpty(jwtSecret))
        {
            throw new ApplicationException("JwtSecret não está definida como uma variável de ambiente.");
        }

        var key = Encoding.ASCII.GetBytes(jwtSecret);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, user.Name.ToString()),
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, "registered"),
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
