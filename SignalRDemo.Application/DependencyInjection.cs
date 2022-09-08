using Microsoft.Extensions.DependencyInjection;
using SignalRDemo.Authorization.Accounts;
using SignalRDemo.Cars;
using SignalRDemo.Orders;
using SignalRDemo.System;

namespace SignalRDemo;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IAccountAppService, AccountAppService>();
        services.AddTransient<ICarAppService, CarAppService>();
        services.AddTransient<IOrderAppService, OrderAppService>();
        services.AddTransient<SampleDataSeeder>();

        return services;
    }
}
