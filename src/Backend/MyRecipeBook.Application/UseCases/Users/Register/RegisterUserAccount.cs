using FluentValidation.Results;
using Mapster;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.PasswordHashing;
using MyRecipeBook.Exception;
using MyRecipeBook.Exception.ExceptionBase;


namespace MyRecipeBook.Application.UseCases.Users.Register;

public class RegisterUserAccount: IRegisterUserAccount
{

    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserWriteOnlyRepository _useWriteOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterUserAccount(IPasswordHasher passwordHasher, IUserWriteOnlyRepository repository, IUserReadOnlyRepository userReadOnlyRepository, IUnitOfWork unitOfWork) 
    {
        _passwordHasher = passwordHasher;
        _useWriteOnlyRepository = repository;
        _unitOfWork = unitOfWork; 
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserAccountJson request)
    {
        await ValidateAndThrowOnFailures(request);
            
        var user = request.Adapt<Domain.Entities.User>();

        user.Password = _passwordHasher.HashPassword(request.Password);

        await _useWriteOnlyRepository.Add(user);

        await _unitOfWork.Commit();

        var userCreated = new ResponseRegisteredUserJson()
        {
            Name = request.Name,
            Tokens = new ResponseTokensJson()
        };

        return userCreated;
    }


    private async Task ValidateAndThrowOnFailures(RequestRegisterUserAccountJson request)
    {
        var validator = new RegisterUserAccountValidator();

        var result = validator.Validate(request);

        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if(emailExist)
        {
            result.Errors.Add(new ValidationFailure(string.Empty ,ResourceMessagesException.VALIDATION_EMAIL_ALREADY_EXISTS));
        }

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
