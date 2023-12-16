using Application.Cross.DependencyInjections;
using Application.MediatR.Commands;
using Domain.DTO;
using Domain.Interfaces.Repositories;
using Infraestructure.Services;
using MediatR;

namespace Application.MediatR.Handlers
{
    public class GetAuthenticationTokenHandler : IRequestHandler<GetAuthenticationTokenCommand, UserTokenDTO>
    {
        private readonly IUserRepository _repository;
        private readonly IAuthenticationService _authenticationService;

        public GetAuthenticationTokenHandler(IUserRepository repository, IAuthenticationService authenticationService)
        {
            _repository = repository;
            _authenticationService = authenticationService;
        }

        public async Task<UserTokenDTO> Handle(GetAuthenticationTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByLogin(request.Email, request.Password);

            if (user == null)
                throw new UserNotFoundException($"user_not_found");

            var token = _authenticationService.CreateAuthenticationToken(user);

            return new UserTokenDTO
            {
                User = user,
                Token = token
            };
        }
    }
}
