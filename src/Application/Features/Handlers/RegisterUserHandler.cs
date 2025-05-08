namespace Application.Features.Handlers
{
    public class RegisterUserHandler(IUserRepository repository) : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _repository = repository;

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _repository.GetUsersByEmail(request.Email);

            if (existingUser != null)
                throw new Exception($"user_already_exist");

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                CPF = request.CPF,
                Phone = request.Phone
            };

            var result = await _repository.Add(user);
            return result.Id;
        }
    }
}
