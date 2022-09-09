using SignalRDemo.Application;

namespace SignalRDemo.Cars
{
    public class CarImageDto : EntityDto
    {
        public int CarId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public CarImageType CarImageType { get; set; }
        public string Url { get; set; }
    }
}
