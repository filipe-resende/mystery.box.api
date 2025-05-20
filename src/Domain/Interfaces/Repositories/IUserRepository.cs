namespace Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByLogin(string email, string password);
    Task<User?> GetUserByEmail(string email);
}