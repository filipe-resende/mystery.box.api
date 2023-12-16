using Application.DBContext;
using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
    public class UserRepository : Repository<User, UserDTO>, IUserRepository
    {
        public readonly IMapper _mapper;
        public UserRepository(Context dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _mapper = mapper;
        }

        public async Task<UserDTO> GetUserByLogin(string email, string password)
        {
            var entity = await dbContext.Set<User>()
                .FirstOrDefaultAsync(e => e.Email == email && e.Password == password);

            var mappedEntity = _mapper.Map<UserDTO>(entity);
            return mappedEntity;
        }

        public async Task<UserDTO> GetUsersByEmail(string email)
        {
            var entity = await dbContext.Set<User>()
              .FirstOrDefaultAsync(e => e.Email == email);

            var mappedEntity = _mapper.Map<UserDTO>(entity);
            return mappedEntity;
        }
    }
}
