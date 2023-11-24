using AutoMapper;
using CoffeeService.Dtos;
using CoffeeService.Models;
using UserService;

namespace CoffeeService.Profiles;

//defines how AutoMapper will map objects
public class CoffeeProfile : Profile
{
    public CoffeeProfile()
    {
        //map coffee to coffeeReadDto
        CreateMap<Coffee, CoffeeReadDto>();

        //map coffeeCreateDto to coffee 
        CreateMap<CoffeeCreateDto, Coffee>();

        //map coffeeReadDto to coffeePublishDto
        CreateMap<CoffeeReadDto, CoffeePublishDto>();

        //map coffee to coffeeUserModel (generated)
        CreateMap<Coffee, GrpcCoffeeModel>();
    }
}