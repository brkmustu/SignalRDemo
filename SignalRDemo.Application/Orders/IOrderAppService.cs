using SignalRDemo.Application;

namespace SignalRDemo.Orders;

public interface IOrderAppService : IApplicationService<Order, OrderDto>
{
}
