using Mapster;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Security.PasswordHashing;
using MyRecipeBook.Exception;
using MyRecipeBook.Exception.ExceptionBase;


namespace MyRecipeBook.Application.UseCases.Users.Register;

public class RegisterUserAccount: IRegisterUserAccount
{

    private readonly IPasswordHasher _passwordHasher;
    public RegisterUserAccount(IPasswordHasher passwordHasher) 
    {
        _passwordHasher = passwordHasher;
    }

    public void Execute(RequestRegisterUserAccountJson request)
    {
        ValidateAndThrowOnFailures(request);

        var user = request.Adapt<Domain.Entities.User>();

        user.Password = _passwordHasher.HashPassword(request.Password);    
    }


    private void ValidateAndThrowOnFailures(RequestRegisterUserAccountJson request)
    {
        var validator = new RegisterUserAccountValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
