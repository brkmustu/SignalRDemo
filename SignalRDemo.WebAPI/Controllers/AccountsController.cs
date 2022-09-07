using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRDemo.Authorization.Accounts;

namespace SignalRDemo.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class AccountsController : Controller
{
    private readonly IAccountAppService _accountAppService;

    public AccountsController(IAccountAppService accountAppService)
    {
        _accountAppService = accountAppService;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> SignInAsync(SignInRequestDto request)
    {
        var result = await _accountAppService.SignInAsync(request);
        return new OkObjectResult(result);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> RegisterAsync(RegisterRequestDto request)
    {
        var result = await _accountAppService.RegisterAsync(request);
        return new OkObjectResult(result);
    }
}
