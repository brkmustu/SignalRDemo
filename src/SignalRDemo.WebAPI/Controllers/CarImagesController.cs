using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRDemo.Cars;

namespace SignalRDemo.WebAPI.Controllers;

[Authorize(Roles = "User")]
public class CarImagesController : BaseController
{
    private readonly ICarImageAppService _carImageAppService;

    public CarImagesController(ICarImageAppService carImageAppService)
    {
        _carImageAppService = carImageAppService;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAllAsync()
    {
        var cars = await _carImageAppService.GetAllAsync();
        return Ok(cars);
    }
}
