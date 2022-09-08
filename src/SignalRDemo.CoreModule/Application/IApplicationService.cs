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
    Task<TEntity> InsertAsync(TEntityDto entity);
    Task<TEntity> GetByIdAsync(int id);
    Task<List<TEntity>> GetAllAsync();
}
