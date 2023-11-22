using AutoMapper;
using CoffeeService;
using FavoriteService.Dtos;
using FavoriteService.Models;

namespace FavoriteService.Profiles;

public class CoffeeProfile : Profile
{
    public CoffeeProfile()
    {
        //map coffee to coffeeReadDto
        //coffee.ExternalId is mapped to coffeeReadDto.id
        CreateMap<Coffee, CoffeeReadDto>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.ExternalId));

        //map coffeePublishDto to coffee
        //coffeePublishDto.Id is mapped to coffee.ExternalId
        CreateMap<CoffeePublishDto, Coffee>()
            .ForMember(dest => dest.ExternalId,
                opt => opt.MapFrom(src => src.Id));

        //map grpcCoffeeModel to coffee (generated)
        CreateMap<GrpcCoffeeModel, Coffee>()
            .ForMember(dest => dest.ExternalId,
                opt => opt.MapFrom(src => src.Id));
    }
}