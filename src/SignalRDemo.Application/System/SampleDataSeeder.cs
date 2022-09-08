using SignalRDemo.Authorization.Roles;
using SignalRDemo.Authorization.Users;
using SignalRDemo.Cars;
using SignalRDemo.DataAccess;

namespace SignalRDemo.System;

public class SampleDataSeeder
{
    private readonly IGenericRepository<Car> _carRepository;
    private readonly IGenericRepository<CarImage> _carImageRepository;
    private readonly IGenericRepository<Role> _roleRepository;
    private readonly IGenericRepository<User> _userRepository;

    private const string AdminPassword = "admin";

    public SampleDataSeeder(
            IGenericRepository<Car> carRepository,
            IGenericRepository<CarImage> carImageRepository,
            IGenericRepository<Role> roleRepository,
            IGenericRepository<User> userRepository
        )
    {
        _carRepository = carRepository;
        _carImageRepository = carImageRepository;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
    }

    public async Task SeedSampleDatasAsync()
    {
        await SeedCarsAsync();
        await SeedCarImagesAsync();
        await SeedRolesAsync();
        await SeedUsersAsync();
    }

    private async Task SeedCarsAsync()
    {
        var cars = await _carRepository.GetAllAsync();

        if (cars.Any())
        {
            return;
        }

        await _carRepository.InsertAsync(new Car
        {
            Id = 1,
            Code = "bmw",
            Name = "BMW i8",
            ImageUrl = "/src/assets/img/bmw-i8.jpg",
            Description = "Bmw i8'i hibrit motor ile performansı hissedin"
        });
        await _carRepository.InsertAsync(new Car
        {
            Id = 2,
            Code = "mercedes",
            Name = "Mercedes EQS",
            ImageUrl = "/src/assets/img/mercedes-eqs.jpeg",
            Description = "Mercedes EQS ile elektrikli motorun keyfini çıkarın"
        });
    }

    private async Task SeedCarImagesAsync()
    {
        var carImages = await _carImageRepository.GetAllAsync();

        if (carImages.Any())
        {
            return;
        }

        await _carImageRepository.InsertAsync(new CarImage
        {
            Id = 1,
            CarId = 1,
            CarImageType = CarImageType.Default,
            Url = "/src/assets/img/bmw-i8.jpg",
        });
        await _carImageRepository.InsertAsync(new CarImage
        {
            Id = 1,
            CarId = 1,
            CarImageType = CarImageType.OnlyDoorsOpen,
            Url = "/src/assets/img/bmw-i8-left-door.jpg",
        });
        await _carImageRepository.InsertAsync(new CarImage
        {
            Id = 1,
            CarId = 1,
            CarImageType = CarImageType.OnlyLigthOpen,
            Url = "/src/assets/img/bmw-i8-light.jpg",
        });

        await _carImageRepository.InsertAsync(new CarImage
        {
            Id = 1,
            CarId = 1,
            CarImageType = CarImageType.DoorsAndLigthBothOpen,
            Url = "/src/assets/img/bmw-i8-light-and-doors.jpg",
        });

        await _carImageRepository.InsertAsync(new CarImage
        {
            Id = 1,
            CarId = 2,
            CarImageType = CarImageType.Default,
            Url = "/src/assets/img/mercedes-eqs.jpeg",
        });
        await _carImageRepository.InsertAsync(new CarImage
        {
            Id = 1,
            CarId = 2,
            CarImageType = CarImageType.OnlyDoorsOpen,
            Url = "/src/assets/img/mercedes-eqs-doors.webp",
        });
        await _carImageRepository.InsertAsync(new CarImage
        {
            Id = 1,
            CarId = 2,
            CarImageType = CarImageType.OnlyLigthOpen,
            Url = "/src/assets/img/mercedes-eqs-light.jpeg",
        });
        await _carImageRepository.InsertAsync(new CarImage
        {
            Id = 1,
            CarId = 2,
            CarImageType = CarImageType.DoorsAndLigthBothOpen,
            Url = "/src/assets/img/mercedes-eqs-doors-and-light.jpeg",
        });

    }
    private async Task SeedRolesAsync()
    {
        var roles = await _roleRepository.GetAllAsync();

        if (roles.Any())
        {
            return;
        }

        await _roleRepository.InsertAsync(new Role
        {
            Id = 1,
            Name = "Admin",
            Description = "Admin yetkisi için varsayılan rol."
        });
        await _roleRepository.InsertAsync(new Role
        {
            Id = 1,
            Name = "User",
            Description = "Kayıt olan kullanıcılara tanımladığımız varsayılan rol."
        });
    }
    private async Task SeedUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();

        if (users.Any())
        {
            return;
        }

        var user = new User
        {
            Id = 1,
            EmailAddress = "admin@admin.com",
            CreatedDate = DateTime.Now,
            RoleIds = new int[] { 1, 2 },
        };

        var encryptedPassword = AdminPassword.CreatePasswordHash();
        user.PasswordHash = encryptedPassword.PasswordHash;
        user.PasswordSalt = encryptedPassword.PasswordSalt;

        await _userRepository.InsertAsync(user);
    }
}
