using AutoMapper;
using SignalRDemo.DataAccess;

namespace SignalRDemo.Orders;

public class OrderAppService : ApplicationService<Order, OrderDto>, IOrderAppService
{
    public OrderAppService(IGenericRepository<Order> repository, IMapper mapper)
        : base(repository, mapper)
    {
    }
}
