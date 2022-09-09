using AutoMapper;
using SignalRDemo.Application;
using SignalRDemo.DataAccess;

namespace SignalRDemo;

public abstract class ApplicationService<TEntity, TEntityDto> : IApplicationService<TEntity, TEntityDto>
    where TEntity : class, IEntity
    where TEntityDto : class, IEntityDto
{
    protected readonly IMapper _mapper;
    protected readonly IGenericRepository<TEntity> _repository;

    public ApplicationService(IGenericRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TEntityDto> InsertAsync(TEntityDto entityDto)
    {
        var entity = _mapper.Map<TEntity>(entityDto);
        await _repository.InsertAsync(entity);
        return _mapper.Map<TEntityDto>(entity);
    }

    public async Task<List<TEntityDto>> GetAllAsync()
    {
        var result = new List<TEntityDto>();
        var entities = await _repository.GetAllAsync();
        foreach (var entity in entities)
            result.Add(_mapper.Map<TEntityDto>(entity));
        return result;
    }

    public async Task<TEntityDto> GetByIdAsync(int id)
    {
        var car = await _repository.GetAsync(id);
        return _mapper.Map<TEntityDto>(car);
    }

    public async Task<TEntityDto> UpdateAsync(TEntityDto entityDto)
    {
        var entity = _mapper.Map<TEntity>(entityDto);
        await _repository.UpdateAsync(entity);
        return _mapper.Map<TEntityDto>(entity);
    }
}
