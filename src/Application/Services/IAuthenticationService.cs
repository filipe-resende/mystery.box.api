using Domain.Entities;

namespace Application.Services;

public interface IAuthenticationService
{
    string CreateAuthenticationToken(User userDto);
}
