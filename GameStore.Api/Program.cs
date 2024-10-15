using GameStore.Api.Authorization;
using GameStore.Api.Cors;
using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using GameStore.Api.ErrorHandling;
using GameStore.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories(builder.Configuration); // extension for repositories

builder.Services.AddAuthentication()
                .AddJwtBearer()
                .AddJwtBearer("Auth0");

builder.Services.AddGameStoreAuthorization(); // extension for authorization
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new(1.0);
    options.AssumeDefaultVersionWhenUnspecified = true;
});

builder.Services.AddHttpLogging(o => { });

builder.Services.AddGameStoreCors(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler(ExceptionHandlerApp => ExceptionHandlerApp.ConfigureExceptionHandler());

app.UseMiddleware<RequestTimingMiddleware>(); // custom middleware 

await app.Services.InitializeDbAsync(); // extension for Db Migrations

app.UseHttpLogging();
app.MapGamesEndpoints(); // extension for HTTP methods

app.UseCors();

app.Run();