using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MyRecipeBook.Infrastructure.DataAcess.Repositories;

internal sealed class UserRepository: IUserWriteOnlyRepository, IUserReadOnlyRepository
{
    private readonly MyRecipeBookDbContext _dbContext;

    public UserRepository(MyRecipeBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(User user) => await _dbContext.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail(string email) => 
        await _dbContext.Users.AnyAsync(user => user.Active && user.Email.Equals(email));
}
