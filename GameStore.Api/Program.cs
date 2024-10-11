using GameStore.Api.Authorization;
using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories(builder.Configuration); // extension for repositories

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddGameStoreAuthorization(); // extension for authorization

builder.Services.AddHttpLogging(o => { });

var app = builder.Build();

await app.Services.InitializeDbAsync(); // extension for Db Migrations

app.UseHttpLogging();
app.MapGamesEndpoints(); // extension for HTTP methods

app.Run();