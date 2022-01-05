using NeoBoilerplate.Application.Contracts.Persistence;
using NeoBoilerplate.Infrastructure.EncryptDecrypt;
using NeoBoilerplate.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NeoBoilerplate.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var dbProvider = configuration.GetValue<string>("dbProvider");

            switch (dbProvider)
            {
                case "MSSQL":
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("GloboTicketTicketManagementConnectionString")));
                    break;
                case "PGSQL":
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseNpgsql(configuration.GetConnectionString("GloboTicketTicketManagementConnectionString")));
                    break;
                case "MySQL":
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseMySql(configuration.GetConnectionString("GloboTicketTicketManagementConnectionString"),
            new MySqlServerVersion(new Version(8, 0, 11))
            ));
                    break;
                default:
                    break;
            }

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();

            return services;
        }
    }
}
