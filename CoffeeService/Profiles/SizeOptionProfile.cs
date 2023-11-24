using AutoMapper;
using CoffeeService.Dtos;
using CoffeeService.Models;

namespace CoffeeService.Profiles;

//defines how AutoMapper will map objects
public class SizeOptionProfile : Profile
{
    public SizeOptionProfile()
    {
        //map sizeOption to sizeOptionReadDto
        CreateMap<SizeOption, SizeOptionReadDto>();

        //map sizeOptionCreateDto to sizeOption 
        CreateMap<SizeOptionCreateDto, SizeOption>();
    }
}