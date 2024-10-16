using GameStore.Api.Authorization;
using GameStore.Api.Cors;
using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using GameStore.Api.ErrorHandling;
using GameStore.Api.Middleware;
using GameStore.Api.OpenAPI;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

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
})
.AddApiExplorer(options => options.GroupNameFormat = "'v'VVV");

builder.Services.AddSwaggerGen()
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
                .AddEndpointsApiExplorer();

builder.Services.AddHttpLogging(o => { });

builder.Services.AddGameStoreCors(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler(ExceptionHandlerApp => ExceptionHandlerApp.ConfigureExceptionHandler());

app.UseMiddleware<RequestTimingMiddleware>(); // custom middleware 

await app.Services.InitializeDbAsync(); // extension for Db Migrations

app.UseHttpLogging();
app.MapGamesEndpoints(); // extension for HTTP methods

app.UseCors();

app.UseGameStoreSwagger();

app.Run();