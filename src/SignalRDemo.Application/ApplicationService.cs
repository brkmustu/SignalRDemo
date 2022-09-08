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

    public async Task<TEntity> InsertAsync(TEntityDto entityDto)
    {
        var entity = _mapper.Map<TEntity>(entityDto);
        await _repository.InsertAsync(entity);
        return entity;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return (List<TEntity>)await _repository.GetAllAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _repository.GetAsync(id);
    }
}
