using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Application.UseCases.Users.Register;

public interface IRegisterUserAccount
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserAccountJson request);
}
