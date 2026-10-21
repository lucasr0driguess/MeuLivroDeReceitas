using Moq;
using MyRecipeBook.Domain.Repositories.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommomTestUtilities.Repositories;

public class IUserWriteOnlyRepositoryBuilder
{

    public static IUserWriteOnlyRepository Build()
    {
        var mock = new Mock<IUserWriteOnlyRepository>();

        return mock.Object;
    }

}
