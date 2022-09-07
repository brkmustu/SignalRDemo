using SignalRDemo.DataAccess;

namespace SignalRDemo.Authorization.Roles;

public class RolePermissions : ValueObject, IEntity
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public int PermissionId { get; set; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return RoleId;
        yield return PermissionId;
    }
}
