using SignalRDemo.Cars;

namespace SignalRDemo.WebAPI.Hubs;

public interface ICarImageHub
{
    Task ChangeImage(UpdateCarImageDto input);
}