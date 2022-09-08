using SignalRDemo.Application;

namespace SignalRDemo.Cars
{
    public class CarImageDto : EntityDto
    {
        public string ImageUrl { get; set; }
        public CarImageType Type { get; set; }
    }
}
