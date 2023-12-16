using Application.Cross.DependencyInjections;
using Application.MediatR.Commands;
using Domain;
using Domain.DTO;
using Domain.Interfaces.Repositories;
using Infraestructure.Services;
using MediatR;

namespace Application.MediatR.Handlers
{
    public class GetForgottenUserHandler : IRequestHandler<GetForgottenUserCommand, ErrorResponseDTO>
    {
        private readonly IEmailService _emailService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRepository _userRepository;

        public GetForgottenUserHandler(IEmailService emailService, IAuthenticationService authenticationService, IUserRepository userRepository)
        {
            _emailService = emailService;
            _authenticationService = authenticationService;
            _userRepository = userRepository;
        }

        public async Task<ErrorResponseDTO> Handle(GetForgottenUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUsersByEmail(request.Email);

                if (user == null)
                    throw new UserNotFoundException($"user_not_found");

                var token = _authenticationService.CreateAuthenticationToken(user);

                _emailService.SendEmail(
                emailTo: request.Email,
                subject: $"[Recuperação de Senha] {request.Email} - {DateTime.Now}",
                body: $@"Clique no link a seguir para redefinir sua senha: <a href='http://localhost:3000/login/reset/{token}'>Redefinir Senha</a>");
            }
            catch (Exception e)
            {
                throw;
            }

            return new ErrorResponseDTO { Status = 200, Error = "", Message = "Email Enviado!" };

        }
    }
}
