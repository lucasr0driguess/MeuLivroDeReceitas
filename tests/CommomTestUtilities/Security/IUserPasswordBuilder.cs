using Moq;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.PasswordHashing;

namespace CommomTestUtilities.Security;

public class IUserPasswordBuilder
{

    private readonly Mock<IPasswordHasher> _mock;


    public IUserPasswordBuilder()
    {
        _mock = new Mock<IPasswordHasher>();

        _mock.Setup(passwordHasher => passwordHasher.HashPassword(It.IsAny<string>())).Returns("hashed_passowrd");
    }

    public void ExistsActiverUserEmail(string password)
    {
        _mock.Setup(repository => repository.VerifyPassword(password, It.IsAny<string>())).Returns(true);
    }

    public IPasswordHasher Build() => _mock.Object;


}
