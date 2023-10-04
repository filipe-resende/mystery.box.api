using Domain.DTO;
using MediatR;

namespace Application.MediatR.Commands
{
    public class GetUserCommand : IRequest<UserDTO>
    {
        public GetUserCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}