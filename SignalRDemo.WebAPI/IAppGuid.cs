namespace SignalRDemo.WebAPI;

public interface IAppGuid
{
    Guid AppId { get; }
}

public class AppGuid : IAppGuid
{
    private Guid _appId = Guid.NewGuid();
    public Guid AppId => _appId;
}