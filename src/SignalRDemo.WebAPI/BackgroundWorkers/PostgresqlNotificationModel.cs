namespace SignalRDemo.WebAPI.BackgroundWorkers;

public class PostgresqlNotificationModel<TData>
{
    public string table { get; set; }
    public string action { get; set; }
    public TData data { get; set; }
}
