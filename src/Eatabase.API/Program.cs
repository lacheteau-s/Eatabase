using Eatabase.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices();

var app = builder.Build();

app.ConfigureMiddlewares();
app.ConfigureEndpoints();

app.Run();
