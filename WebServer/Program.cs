using HamsterCoin.Domain;
using HamsterCoin.Extensions;
using HamsterCoin.Services.Implementations;
using HamsterCoin.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDatasource(builder.Configuration);

builder.Services.AddScoped<IUserDetailsService, UserDetailsService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
