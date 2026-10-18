using MyRecipeBook.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Infrastructure.DataAcess;

internal class UnitOfWork: IUnitOfWork
{

    private readonly MyRecipeBookDbContext _dbContext;

    public UnitOfWork(MyRecipeBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task Commit() => await _dbContext.SaveChangesAsync();

}
