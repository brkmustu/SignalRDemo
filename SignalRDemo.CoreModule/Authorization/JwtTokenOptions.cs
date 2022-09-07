namespace SignalRDemo.Authorization;

public class JwtTokenOptions
{
    public const string SectionName = "TokenOptions";
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public int AccessTokenExpiration { get; set; }
    public string SecurityKey { get; set; }
}