using AutoMapper;
using SignalRDemo.DataAccess;

namespace SignalRDemo.Cars;

public class CarAppService : ApplicationService<Car, CarDto>, ICarAppService
{
    public CarAppService(IGenericRepository<Car> repository, IMapper mapper)
        : base(repository, mapper)
    {
    }
}
