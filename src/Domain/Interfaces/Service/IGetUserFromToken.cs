namespace Domain.Interfaces.Service;

public interface IGetUserFromToken
{
    Task<UserDTO> GetUserIdFromToken();
}
