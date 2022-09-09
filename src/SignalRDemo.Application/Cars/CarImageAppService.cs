using AutoMapper;
using SignalRDemo.DataAccess;

namespace SignalRDemo.Cars;

public class CarImageAppService : ApplicationService<CarImage, CarImageDto>, ICarImageAppService
{
    public CarImageAppService(IGenericRepository<CarImage> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
