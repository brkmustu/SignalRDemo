using SignalRDemo.Application;

namespace SignalRDemo.Orders
{
    public class OrderDto : AuditableEntityDto
    {
        public int CarId { get; set; }
    }
}
