using Application.Configurations;
using Application.MediatR.Commands;
using Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.MediatR.Handlers
{
    public class GetAuthenticationTokenHandler : IRequestHandler<GetAuthenticationTokenCommand, string>
    {
        private readonly IUserRepository _repository;

        public GetAuthenticationTokenHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(GetAuthenticationTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByLogin(request.Email, request.Password);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
