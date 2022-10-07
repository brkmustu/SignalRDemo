using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using SignalRDemo.Authorization.Accounts;
using SignalRDemo.Cars;
using SignalRDemo.Orders;
using SignalRDemo.System;
using SignalRDemo.System.Aspects;
using Stashbox;

namespace SignalRDemo;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IAccountAppService, AccountAppService>();
        services.AddTransient<ICarImageAppService, CarImageAppService>();
        services.AddTransient<IOrderAppService, OrderAppService>();
        services.AddTransient<SampleDataSeeder>();

        return services;
    }

    public static IStashboxContainer AddApplication(this IStashboxContainer container)
    {
        var proxyBuilder = new DefaultProxyBuilder();

        container.Register<ICarAppService, CarAppService>();
        container.Register<IAccountAppService, AccountAppService>();
        container.Register<ICarImageAppService, CarImageAppService>();
        container.Register<IOrderAppService, OrderAppService>();

        container.Register<IInterceptor, ExceptionHandlingInterceptor>();

        container.RegisterDecorator<ICarAppService>(proxyBuilder);
        container.RegisterDecorator<IAccountAppService>(proxyBuilder);
        container.RegisterDecorator<ICarImageAppService>(proxyBuilder);
        container.RegisterDecorator<IOrderAppService>(proxyBuilder);

        return container;
    }

    /// <summary>
    /// normalde tam da bu esnada servisin bir interceptor'e dahiliyeti var mı yok mu kontrolünü yapmak gerek.
    /// reflection üzerinden attribute'leri kullanılarak yapılabilir. şimdilik bu haliyle bırakıyoruz.
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="container"></param>
    /// <param name="proxyBuilder"></param>
    /// <returns></returns>
    private static IStashboxContainer RegisterDecorator<TService>(this IStashboxContainer container, DefaultProxyBuilder proxyBuilder)
        where TService : class
    {
        var currentProxy = proxyBuilder.CreateInterfaceProxyTypeWithTargetInterface(
            typeof(TService),
            new Type[0],
            ProxyGenerationOptions.Default);

        container.RegisterDecorator<TService>(currentProxy);

        return container;
    }
}
