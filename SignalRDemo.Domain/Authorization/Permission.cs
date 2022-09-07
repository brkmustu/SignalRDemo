using System.Diagnostics.CodeAnalysis;

namespace SignalRDemo.Authorization;

public class Permission : Entity
{
    public string Name { get; set; }
}

public class PermissionNameComparer : IEqualityComparer<Permission>
{
    public bool Equals(Permission? x, Permission? y)
    {
        return x.Name == y.Name;
    }

    public int GetHashCode([DisallowNull] Permission obj)
    {
        return obj.Name.GetHashCode();
    }
}
