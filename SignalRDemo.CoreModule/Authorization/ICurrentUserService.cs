using System.Security.Principal;

namespace SignalRDemo.Authorization;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    IIdentity Identity { get; }
    bool IsInRole(string role);
}
