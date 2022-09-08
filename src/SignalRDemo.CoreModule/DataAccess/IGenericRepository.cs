namespace SignalRDemo.DataAccess;

public interface IGenericRepository<T> where T : class, IEntity
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T> GetAsync(object id, CancellationToken cancellationToken = default);
    Task InsertAsync(T t, CancellationToken cancellationToken = default);
    Task UpdateAsync(T t, CancellationToken cancellationToken = default);
    Task DeleteAsync(object id, CancellationToken cancellationToken = default);
}
