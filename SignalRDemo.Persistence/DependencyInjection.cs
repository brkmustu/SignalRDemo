using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignalRDemo.DapperImplementation;
using SignalRDemo.DataAccess;

namespace SignalRDemo;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication();

        services.AddTransient(typeof(IGenericRepository<>), typeof(DapperGenericRepository<>));

        string connectionString = configuration.GetDefaultConnectionString();

        services.AddDbContext<SignalRDemoDbContext>(options => options.UseNpgsql(connectionString));

        return services;
    }

    public static string GetDefaultConnectionString(this IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings_SignalRDemo");
        if (string.IsNullOrEmpty(connectionString))
        {
            connectionString = configuration.GetConnectionString("SignalRDemo");
        }
        return connectionString;
    }
}

