using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRDemo.Cars;

namespace SignalRDemo.WebAPI.Controllers;

[Authorize(Roles = "User")]
public class CarsController : BaseController
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

    [HttpPut]
    [Route("[action]")]
    public async Task<IActionResult> UpdateImageAsync([FromBody] UpdateCarImageDto input)
    {
        var car = await _carAppService.UpdateCarImageAsync(input);
        return Ok(car);
    }
}
