namespace SignalRDemo.Cars;

public class Car : Entity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }

    public ICollection<CarImage> Images { get; set; }
}
