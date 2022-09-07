using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRDemo.Cars;

namespace SignalRDemo.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CarsController : Controller
{
    private readonly ICarAppService _carAppService;

    public CarsController(ICarAppService carAppService)
    {
        _carAppService = carAppService;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAllAsync()
    {
        var cars = await _carAppService.GetAllAsync();
        return Ok(cars);
    }
}
