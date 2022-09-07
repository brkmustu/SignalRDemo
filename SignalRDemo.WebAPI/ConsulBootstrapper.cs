using Consul;
using SignalRDemo.WebAPI;

public static class ConsulBootstrapper
{
    public static IServiceCollection AddConsuleClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConsulClient, ConsulClient>(x => new ConsulClient(consulConfig =>
        {
            var address = "http://localhost:8500";
            consulConfig.Address = new Uri(address);
        }));

        return services;
    }
    public static IApplicationBuilder RegisterWithConsule(this IApplicationBuilder app, ICollection<string> urls)
    {
        var consuleClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
        var appGuid = app.ApplicationServices.GetRequiredService<IAppGuid>();

        var loggingFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();

        var logger = loggingFactory.CreateLogger<IApplicationBuilder>();

        var address = "http://localhost:5000";

        var uri = new Uri(address);
        var registration = new AgentServiceRegistration
        {
            ID = $"{uri.Host}:{appGuid.AppId}",
            Name = $"SignalRDemoApi",
            Address = $"{uri.Host}",
            Port = uri.Port,
            Tags = new[] { "SignalR Demo Service", "Portal" }
        };

        logger.LogInformation("Registering with Consul");
        consuleClient.Agent.ServiceDeregister(registration.ID).Wait();
        consuleClient.Agent.ServiceRegister(registration).Wait();

        return app;
    }

    public static IApplicationBuilder DeregisterWithConsule(this IApplicationBuilder app, ICollection<string> urls)
    {
        var consuleClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
        var appGuid = app.ApplicationServices.GetRequiredService<IAppGuid>();

        var loggingFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();

        var logger = loggingFactory.CreateLogger<IApplicationBuilder>();

        var address = "http://localhost:5000";

        var uri = new Uri(address);
        var registration = new AgentServiceRegistration
        {
            ID = $"{uri.Host}:{appGuid.AppId}",
            Name = $"SignalRDemoApi",
            Address = $"{uri.Host}",
            Port = uri.Port,
            Tags = new[] { "SignalR Demo Service", "Portal" }
        };

        logger.LogInformation("Deregistering from Consul");
        consuleClient.Agent.ServiceDeregister(registration.ID).Wait();

        return app;
    }
}
