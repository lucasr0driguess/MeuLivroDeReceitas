using MyRecipeBook.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Application.UseCases.Users.Register;

public interface IRegisterUserAccount
{
    void Execute(RequestRegisterUserAccountJson request);
}
