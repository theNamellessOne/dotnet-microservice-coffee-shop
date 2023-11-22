using AutoMapper;
using UserService.Dtos;
using UserService.Models;

namespace UserService.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        //map user to userReadDto
        CreateMap<User, UserReadDto>();

        //map userReadDto to userPublishDto
        CreateMap<UserReadDto, UserPublishDto>();

        //map userCreateDto to user
        CreateMap<UserCreateDto, User>();

        //map user to grpcUserModel (generated)
        CreateMap<User, GrpcUserModel>();
    }
}