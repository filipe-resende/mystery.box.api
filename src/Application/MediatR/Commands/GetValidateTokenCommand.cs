using Domain.DTO;
using MediatR;

namespace Application.MediatR.Commands
{
    public class GetValidateTokenCommand : IRequest<UserTokenDTO>
    {
        public GetValidateTokenCommand(string userId) 
        {
            UserId = Guid.Parse(userId);
        }
        public Guid UserId { get;}
    }
}
