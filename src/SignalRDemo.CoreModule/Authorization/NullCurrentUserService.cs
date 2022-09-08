using System.Security.Principal;

namespace SignalRDemo.Authorization;

public class NullCurrentUserService : ICurrentUserService
{
    public Guid? UserId => null;

    public bool IsAuthenticated => false;

    public IIdentity Identity => default;

    public bool IsInRole(string role)
    {
        return false;
    }
}
