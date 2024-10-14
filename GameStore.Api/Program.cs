using GameStore.Api.Authorization;
using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using GameStore.Api.ErrorHandling;
using GameStore.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories(builder.Configuration); // extension for repositories

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddGameStoreAuthorization(); // extension for authorization
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new(1.0);
    options.AssumeDefaultVersionWhenUnspecified = true;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsBuilder =>
    {
        var allowerOrigin = builder.Configuration["AllowedOrigin"]
                            ?? throw new InvalidOperationException("AllowedOrigin is not set");
        corsBuilder.WithOrigins(allowerOrigin)
                   .AllowAnyHeader()
                   .AllowAnyMethod();
    });
});

builder.Services.AddHttpLogging(o => { });

var app = builder.Build();

app.UseExceptionHandler(ExceptionHandlerApp => ExceptionHandlerApp.ConfigureExceptionHandler());

app.UseMiddleware<RequestTimingMiddleware>();

await app.Services.InitializeDbAsync(); // extension for Db Migrations

app.UseHttpLogging();
app.MapGamesEndpoints(); // extension for HTTP methods

app.UseCors();

app.Run();