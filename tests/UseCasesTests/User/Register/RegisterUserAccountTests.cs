using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using CommomTestUtilities.Security;
using MyRecipeBook.Application.UseCases.Users.Register;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Security.PasswordHashing;
using MyRecipeBook.Exception;
using MyRecipeBook.Exception.ExceptionBase;
using Shouldly;

namespace UseCases.Tests.User.Register;

public class RegisterUserAccountTests
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserAccountJsonBuilder.Build();

        var useCase = CreateRegisterUserAccount();
        var result = await useCase.Execute(request);

        result.ShouldNotBeNull();
        result.Tokens.ShouldNotBeNull();
        result.Name.ShouldBe(request.Name);
        result.Tokens.AcessToken.ShouldBeNullOrEmpty();
        result.Tokens.RefreshToken.ShouldBeNullOrEmpty();
    }

    [Fact]
    public async Task Validate_ShouldThrowException_WhenNameIsEmpty()
    {
        var request = RequestRegisterUserAccountJsonBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateRegisterUserAccount();
        var exception = await useCase.Execute(request).ShouldThrowAsync<ErrorOnValidationException>();

        
        exception.GetErrorMessages().ShouldSatisfyAllConditions(errorMessages =>
        {
            errorMessages.Count.ShouldBe(1);
            errorMessages.ShouldContain(ResourceMessagesException.VALIDATION_NAME_REQUIRED);
        });
    }

    [Fact]
    public async Task Validate_ShouldThrowException_WhenEmailIsEmpty()
    {
        var request = RequestRegisterUserAccountJsonBuilder.Build();

        var useCase = CreateRegisterUserAccount(request.Email);
        var exception = await useCase.Execute(request).ShouldThrowAsync<ErrorOnValidationException>();


        exception.GetErrorMessages().ShouldSatisfyAllConditions(errorMessages =>
        {
            errorMessages.Count.ShouldBe(1);
            errorMessages.ShouldContain(ResourceMessagesException.VALIDATION_EMAIL_ALREADY_EXISTS);
        });
    }

    private RegisterUserAccount CreateRegisterUserAccount(string? emailThathAlreadyExists = null)
    {
        var unitOfWork = IUnitOfWorkBuilder.Build();
        var userWriteOnlyRepository = IUserWriteOnlyRepositoryBuilder.Build();
        var passwordHasher = new IUserPasswordBuilder().Build();
        var userReadOnlyRepoitory = new IUserReadOnlyRepositoryTests();
        if (emailThathAlreadyExists.IsNotEmpty())
            userReadOnlyRepoitory.ExistsActiverUserEmail(emailThathAlreadyExists);


        return new RegisterUserAccount(passwordHasher, userWriteOnlyRepository, userReadOnlyRepoitory.Build(), unitOfWork);
    }

}
