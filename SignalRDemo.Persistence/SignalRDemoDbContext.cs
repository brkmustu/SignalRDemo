using Microsoft.EntityFrameworkCore;
using SignalRDemo.Authorization.Roles;
using SignalRDemo.Authorization.Users;
using SignalRDemo.Cars;
using SignalRDemo.Orders;

namespace SignalRDemo;

/// <summary>
/// entity framework core'u sadece docker sunucusunda code first ile tabloların oluşturulması amaçlı kullandım.
/// </summary>
public class SignalRDemoDbContext : DbContext
{
    public SignalRDemoDbContext(DbContextOptions<SignalRDemoDbContext> options)
        : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<CarImage> CarImages { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
}
