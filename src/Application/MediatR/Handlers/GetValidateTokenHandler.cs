using Application.MediatR.Commands;
using Domain.DTO;
using Domain.Interfaces.Repositories;
using Infraestructure.Services;
using MediatR;

namespace Application.MediatR.Handlers
{
    public class GetValidateTokenHandler : IRequestHandler<GetValidateTokenCommand, UserTokenDTO>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRepository _repository;

        public GetValidateTokenHandler(IUserRepository repository, IAuthenticationService authenticationService)
        {
            _repository = repository;
            _authenticationService = authenticationService;
        }

        public async Task<UserTokenDTO> Handle(GetValidateTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(request.UserId);
            var token = _authenticationService.CreateAuthenticationToken(user);

            return new UserTokenDTO
            {
                User = user,
                Token = token
            }; ;

        }
    }
}
