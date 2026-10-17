using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.Users.Register;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exception.ExceptionBase;

namespace MyRecipeBook.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    public IActionResult Register([FromBody] RequestRegisterUserAccountJson request, 
                                    [FromServices] IRegisterUserAccount useCase)
    {

        useCase.Execute(request);

        return Created();

    }



}

