using AutoMapper;
using SignalRDemo.DataAccess;

namespace SignalRDemo.Cars;

public class CarAppService : ApplicationService<Car, CarDto>, ICarAppService
{
    public CarAppService(IGenericRepository<Car> repository, IMapper mapper)
        : base(repository, mapper)
    {
    }

    public async Task<CarDto> UpdateCarImageAsync(UpdateCarImageDto dto)
    {
        var car = await _repository.GetAsync(dto.Id);
        car.ImageUrl = dto.Url;
        await _repository.UpdateAsync(car);
        return _mapper.Map<CarDto>(car);
    }
}
