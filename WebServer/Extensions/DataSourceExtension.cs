using Microsoft.EntityFrameworkCore;
using HamsterCoin.Database;

namespace HamsterCoin.Extensions
{
    public static class DataSourceExtensions
    {
        public static void ConfigureDatasource(this IServiceCollection services, IConfiguration configuration)
        {
            // Отримання рядка підключення з appsettings.json
            var connectionString = configuration.GetConnectionString("MyDatabase");

            // Вказуємо версію MySQL сервера (замініть версію на свою)
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            // Додаємо контекст бази даних
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, serverVersion)
            );
        }
    }
}
