using Microsoft.AspNetCore.SignalR;
using Npgsql;
using SignalRDemo.Cars;
using SignalRDemo.WebAPI.Hubs;
using System.Text.Json;

namespace SignalRDemo.WebAPI.BackgroundWorkers;

public class RealTimeCarChangeWorker : BackgroundService
{
    private readonly IHubContext<CarImageHub, ICarImageHub> _hub;
    private readonly string _connectionString;

    public RealTimeCarChangeWorker(IHubContext<CarImageHub, ICarImageHub> hub, IConfiguration configuration)
    {
        _hub = hub;
        _connectionString = configuration.GetConnectionString("SignalRDemo");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var connection = CreateConnection();
        await connection.OpenAsync(stoppingToken);
        connection.Notification += OnNotification;

        await using var command = new NpgsqlCommand("LISTEN datachange;", connection);
        await command.ExecuteNonQueryAsync(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await connection.WaitAsync(stoppingToken);
        }

        await Task.CompletedTask;
    }

    private async void OnNotification(object sender, NpgsqlNotificationEventArgs args)
    {
        var notificationModel = JsonSerializer.Deserialize<PostgresqlNotificationModel<Car>>(args.Payload);
        await _hub.Clients.All.ChangeImage(new UpdateCarImageDto
        {
            Id = notificationModel.data.Id,
            Url = notificationModel.data.ImageUrl
        });
    }

    private NpgsqlConnection SqlConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }

    /// <summary>
    /// Open new connection and return it for use
    /// </summary>
    /// <returns></returns>
    private NpgsqlConnection CreateConnection()
    {
        var conn = SqlConnection();
        return conn;
    }
}
