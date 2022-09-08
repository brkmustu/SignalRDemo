using AutoMapper;
using SignalRDemo.Authorization.Accounts;
using SignalRDemo.Authorization.Users;
using SignalRDemo.Cars;
using SignalRDemo.Orders;

namespace SignalRDemo;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Car, CarDto>();
        CreateMap<CarDto, Car>();

        CreateMap<Order, OrderDto>();
        CreateMap<OrderDto, Order>();

        CreateMap<User, RegisterRequestDto>();
        CreateMap<RegisterRequestDto, User>();

        CreateMap<User, SignInRequestDto>();
        CreateMap<SignInRequestDto, User>();
    }
}
