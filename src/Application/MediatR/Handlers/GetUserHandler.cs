using Application.MediatR.Commands;
using Domain.DTO;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.MediatR.Handlers
{
    public class GetUserHandler : IRequestHandler<GetUserCommand, UserDTO>
    {
        private readonly IUserRepository _repository;

        public GetUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDTO> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _repository.GetById(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"could not be saved. Error {ex.Message}");
            }
        }
    }
}
