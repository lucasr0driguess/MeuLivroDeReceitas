using MyRecipeBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Infrastructure.DataAcess.Repositories;

internal sealed class UserRepository
{
    private readonly MyRecipeBookDbContext _dbContext;

    public UserRepository(MyRecipeBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(User user)
    {
        var userSave = await _dbContext.AddAsync(user);
    }


}
