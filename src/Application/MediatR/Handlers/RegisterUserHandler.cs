using Application.Cross.DependencyInjections.Filter;
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
            var existingUser = await _repository.GetUsersByEmail(request.Email);
                
            if (existingUser != null )
                throw new UserAlreadyExistException($"user_already_exist");

            var user = new UserDTO { 
                Name = request.Name, 
                Email = request.Email, 
                Password = request.Password, 
                CPF = request.CPF,
                Phone = request.Phone};

            var result = await _repository.Add(user);
            return result.Id;
        }
    }
}
