﻿using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByLogin(string email, string password);
    Task<User?> GetUsersByEmail(string email);
}