using CommomTestUtilities.Requests;
using Mapster;
using Microsoft.AspNetCore.Mvc.Testing;
using MyRecipeBook.Api.Controllers;
using System.Net.Http.Json;

namespace WebApi.Tests.User.Register;

public class RegisterUserAccountTests: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    
    
    
    public RegisterUserAccountTests(WebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }


    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserAccountJsonBuilder.Build();

        await _httpClient.PostAsJsonAsync("/users", request);
    }
}
