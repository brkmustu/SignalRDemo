using SignalRDemo.Application;

namespace SignalRDemo.Cars;

public interface ICarAppService : IApplicationService<Car, CarDto>
{
}
