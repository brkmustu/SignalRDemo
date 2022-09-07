using System.ComponentModel.DataAnnotations;
using SignalRDemo.DataAccess;

namespace SignalRDemo.Authorization.Users;

public class UserRoles : ValueObject, IEntity
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public long RoleId { get; set; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return UserId;
        yield return RoleId;
    }
}
