using HamsterCoin.Auth;
using HamsterCoin.OperateException;
using HamsterCoin.Extensions;
using HamsterCoin.Services.Implementations;
using HamsterCoin.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDatasource(builder.Configuration);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen();
    builder.Services.AddEndpointsApiExplorer();
}

builder.Services.AddScoped<IUserDetailsService, UserDetailsService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IWithDrawService, WithDrawService>();
builder.Services.AddScoped<IDepositService, DepositService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IUserCardService, UserCardService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapHamsterCoinEndpoints();

app.Run();
