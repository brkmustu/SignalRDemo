using Consul;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SignalRDemo.Application;
using SignalRDemo.Cars;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SignalRDemo.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
//[Authorize(Roles = "user")]
public class CarsController : Controller
{
    private readonly ICarAppService _carAppService;
    private readonly HttpContext _httpContext;
    private readonly TokenOptions _tokenOptions;

    public CarsController(
        ICarAppService carAppService, 
        IHttpContextAccessor httpContextAccessor,
        IOptions<TokenOptions> options
        )
    {
        _carAppService = carAppService;
        _httpContext = httpContextAccessor.HttpContext;
        _tokenOptions = options.Value;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAllAsync()
    {
        if (_httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            var token = authHeader.ToString().Split(' ')[1];

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = _tokenOptions.Issuer,
                ValidAudience = _tokenOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey)),
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userName = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;
            var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

            //var response = await _client.GetAsync(CommonSettings.GetTokenValidationApiUrl(accessToken));

            //if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    var result = await response.Content.ReadAsStringAsync();
            //    var tokenModel = JsonConvert.DeserializeObject<TokenModel>(result);

            //    return AuthenticateResult.Success(
            //        new AuthenticationTicket(
            //            new ClaimsPrincipal(
            //                new ClaimsIdentity(
            //                    tokenModel.Claims.Select(x => new Claim(x.ClaimType, x.Value)))),
            //        AppConsts.DefaultAuthenticationSchemeName));
            //}
        }

        var cars = await _carAppService.GetAllAsync();
        return Ok(cars);
    }
}
