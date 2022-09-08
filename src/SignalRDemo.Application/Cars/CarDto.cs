using SignalRDemo.Application;

namespace SignalRDemo.Cars
{
    public class CarDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
