using SignalRDemo.DataAccess;

namespace SignalRDemo.Application;

/// <summary>
/// şimdilik örneğimiz için aşağıdaki metodlar yeterli
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IApplicationService<TEntity, TEntityDto> 
    where TEntity : class, IEntity
    where TEntityDto : class, IEntityDto
{
    Task<TEntityDto> InsertAsync(TEntityDto entity);
    Task<TEntityDto> UpdateAsync(TEntityDto entity);
    Task<TEntityDto> GetByIdAsync(int id);
    Task<List<TEntityDto>> GetAllAsync();
}
