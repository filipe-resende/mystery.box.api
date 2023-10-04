using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User, UserDTO>
    {
        Task<UserDTO> GetUserByLogin(string email, string password);
    }
}
