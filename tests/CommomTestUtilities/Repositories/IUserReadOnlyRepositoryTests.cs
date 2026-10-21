using Moq;
using MyRecipeBook.Domain.Repositories.User;

namespace CommomTestUtilities.Repositories;

public class IUserReadOnlyRepositoryTests
{
    private readonly Mock<IUserReadOnlyRepository> _mock;


    public IUserReadOnlyRepositoryTests()
    {
        _mock = new Mock<IUserReadOnlyRepository>();
    }

    public void ExistsActiverUserEmail(string email)
    {
        _mock.Setup(repository => repository.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
    }

    public IUserReadOnlyRepository Build() => _mock.Object;
    

}
