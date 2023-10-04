using Application.MediatR.Commands;
using Domain.DTO;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.MediatR.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _repository;

        public RegisterUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new UserDTO { Name = request.Name, Email = request.Email, Password = request.Password };
                var result = await _repository.Add(user);
                return result.Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"could not be saved. Error {ex.Message}");
            }
        }
    }
}
