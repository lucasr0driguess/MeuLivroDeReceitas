using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.UseCases.Users.Register;

namespace MyRecipeBook.Application;

public static class DependencyInjectionExtension
{

    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRegisterUserAccount, RegisterUserAccount>();
       
    }
}
