using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRDemo.Orders;

namespace SignalRDemo.WebAPI.Controllers;

[Authorize(Roles = "User")]
public class OrdersController : BaseController
{
    private readonly IOrderAppService _orderAppService;

    public OrdersController(IOrderAppService orderAppService)
    {
        _orderAppService = orderAppService;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAllAsync()
    {
        var orders = await _orderAppService.GetAllAsync();
        return new OkObjectResult(orders);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> InsertAsync(OrderDto order)
    {
        var response = await _orderAppService.InsertAsync(order);
        return new OkObjectResult(response);
    }
}
