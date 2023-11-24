using AutoMapper;
using CoffeeService;
using OrderService.Dtos;
using OrderService.Models;

namespace OrderService.Profiles;

//defines how AutoMapper will map objects
public class CoffeeProfile : Profile
{
    public CoffeeProfile()
    {
        //map coffeePublishDto to coffee
        CreateMap<CoffeePublishDto, Coffee>();

        //map coffee to coffeePublishDto
        CreateMap<Coffee, CoffeeReadDto>();

        //map coffeeHttpGetDto to coffee
        CreateMap<CoffeeHttpGetDto, Coffee>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ExternalId,
                opt => opt.MapFrom(src => src.Id));

        //map grpcCoffeeModel to coffee (generated)
        CreateMap<GrpcCoffeeModel, Coffee>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ExternalId,
                opt => opt.MapFrom(src => src.Id));
    }
}