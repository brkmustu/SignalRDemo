using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Cars;

namespace SignalRDemo.WebAPI.Hubs;

public class CarImageHub : Hub<ICarImageHub>
{
    public async Task ChangeImage(UpdateCarImageDto input)
    {
        await Clients.Others.ChangeImage(input);
    }
}
