using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRDemo.Cars;

public class CarImage : Entity
{
    public int CarId { get; set; }
    public CarImageType CarImageType { get; set; }
    public string Url { get; set; }
}

public enum CarImageType
{
    Default = 1,
    OnlyDoorsOpen = 2,
    OnlyLigthOpen = 3,
    DoorsAndLigthBothOpen = 4
}