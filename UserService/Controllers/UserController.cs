using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserService.AsyncDataServices;
using UserService.Data.Repositories;
using UserService.Dtos;
using UserService.Models;

namespace UserService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(
    IMessageBusClient messageBusClient,
    IUserRepository userRepository,
    IMapper mapper
) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<UserReadDto>> GetUsers()
    {
        return Ok(mapper.Map<IEnumerable<UserReadDto>>(userRepository.GetAllUsers()));
    }

    [HttpGet("{id}", Name = "GetUserById")]
    public ActionResult<UserReadDto> GetUserById(int id)
    {
        return Ok(mapper.Map<UserReadDto>(userRepository.GetUserById(id)));
    }

    [HttpPost]
    public ActionResult<UserReadDto> CreateUser(UserCreateDto userCreateDto)
    {
        var userModel = mapper.Map<User>(userCreateDto);
        userRepository.CreateUser(userModel);
        userRepository.SaveChanges();

        var userReadDto = mapper.Map<UserReadDto>(userModel);

        //publish newly created user to message bus for other services to consume
        var userPublishDto = mapper.Map<UserPublishDto>(userReadDto);
        userPublishDto.Event = "User_Created";
        messageBusClient.PublishUser(userPublishDto);

        return CreatedAtRoute(nameof(GetUserById), new { userReadDto.Id }, userReadDto);
    }
}