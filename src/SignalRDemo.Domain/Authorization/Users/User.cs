namespace SignalRDemo.Authorization.Users;

public class User : AuditableEntity
{
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string EmailAddress { get; set; }
    public int[]? RoleIds { get; set; }
}
