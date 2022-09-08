namespace SignalRDemo.Application;

public class TokenResult
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public string[] Roles { get; set; } 
}
