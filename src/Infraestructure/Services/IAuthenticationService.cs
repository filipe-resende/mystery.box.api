using Domain.DTO;

namespace Infraestructure.Services
{
    public interface IAuthenticationService
    {
       string CreateAuthenticationToken(UserDTO userDto);
    }
}
