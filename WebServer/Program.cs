using HamsterCoin.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Отримання рядка підключення з appsettings.json
var connectionString = builder.Configuration.GetConnectionString("MyDatabase");

// Вказуємо версію MySQL сервера (замініть версію на свою)
var serverVersion = ServerVersion.AutoDetect(connectionString);

// Додаємо контекст бази даних
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, serverVersion)
);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
