using Microsoft.Extensions.DependencyInjection;
using SignalRDemo.Authorization.Accounts;
using SignalRDemo.Cars;
using SignalRDemo.Orders;

namespace SignalRDemo;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IAccountAppService, AccountAppService>();
        services.AddTransient<ICarAppService, CarAppService>();
        services.AddTransient<IOrderAppService, OrderAppService>();

        return services;
    }
}
