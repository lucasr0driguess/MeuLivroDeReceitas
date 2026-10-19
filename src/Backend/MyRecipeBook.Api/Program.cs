using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using MyRecipeBook.Api.Filters;
using MyRecipeBook.Api.Converter;
using MyRecipeBook.Application;
using MyRecipeBook.Infrastructure;
using System.Globalization;
using MyRecipeBook.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new StringConverter()));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
//MyRecipeBook.Application.DependencyInjectionExtension.AddApplication(builder.Services);
builder.Services.AddApplication();
//MyRecipeBook.Infrastructure.DependencyInjectionExtension.AddInfraestruct(builder.Services);
builder.Services.AddInfraestructure(builder.Configuration);

builder.Services.Configure<RequestLocalizationOptions>(options => {
    var supportedCultures = new List<CultureInfo> {
        new CultureInfo("en"),
        new CultureInfo("pt-BR"),
        new CultureInfo("es")
    };

    options.DefaultRequestCulture = new RequestCulture("en");


    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;


    options.RequestCultureProviders = [ new AcceptLanguageHeaderRequestCultureProvider() ];
});
builder.Services.AddMvc(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

var localizationService = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();

app.UseRequestLocalization(localizationService.Value);
 
app.UseRequestLocalization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await ExecuteMigrations();

app.Run();

async Task ExecuteMigrations()
{
    await using var scope = app.Services.CreateAsyncScope();

    DatabaseMigrations.ExecuteMigrations(scope.ServiceProvider);
}

public partial class Program { }

 