using SignalRDemo.Application;

namespace SignalRDemo.Authorization.Accounts;

public interface IAccountAppService
{
    Task<Result> RegisterAsync(RegisterRequestDto request);
    Task<Result<TokenResult>> SignInAsync(SignInRequestDto request);
}
