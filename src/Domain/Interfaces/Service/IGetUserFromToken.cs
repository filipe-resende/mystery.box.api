using Domain.DTO;

namespace Domain.Interfaces.Repositories
{
    public interface IGetUserFromToken
    {
        Task<UserDTO> GetUserIdFromToken();
    }
}