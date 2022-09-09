using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Cars;
using SignalRDemo.WebAPI.Hubs;

namespace SignalRDemo.WebAPI.Controllers;

[Authorize(Roles = "User")]
public class CarsController : BaseController
{
    private readonly ICarAppService _carAppService;
    private readonly IHubContext<CarImageHub, ICarImageHub> _hubContext;

    public CarsController(ICarAppService carAppService, IHubContext<CarImageHub, ICarImageHub> hubContext)
    {
        _carAppService = carAppService;
        _hubContext = hubContext;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAllAsync()
    {
        var cars = await _carAppService.GetAllAsync();
        return Ok(cars);
    }

    [HttpPut]
    [Route("[action]")]
    public async Task<IActionResult> UpdateImageAsync([FromBody] UpdateCarImageDto input)
    {
        var car = await _carAppService.UpdateCarImageAsync(input);
        await _hubContext.Clients.All.ChangeImage(input);
        return Ok(car);
    }
}
