namespace Infraestructure.Repository;

public class UserRepository(Context dbContext) : Repository<User>(dbContext), IUserRepository
{
    public async Task<User?> GetUserByLogin(string email, string password)
    {
        return await dbContext.Set<User>()
            .FirstOrDefaultAsync(e => e.Email == email && e.Password == password);
    }

    public async Task<User?> GetUsersByEmail(string email)
    {
        return await dbContext.Set<User>()
          .FirstOrDefaultAsync(e => e.Email == email);
    }
}

