using AutoMapper;
using CoffeeService.AsyncDataServices;
using CoffeeService.Data.Repositories;
using CoffeeService.Dtos;
using CoffeeService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoffeeController(
    IMessageBusClient messageBusClient,
    ICoffeeRepository coffeeRepository,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<CoffeeReadDto>> GetCoffees()
    {
        return Ok(mapper.Map<IEnumerable<CoffeeReadDto>>(coffeeRepository.GetAllCoffees()));
    }

    [HttpGet("{id}", Name = "GetCoffeeById")]
    public ActionResult<CoffeeReadDto> GetCoffeeById(int id)
    {
        return Ok(mapper.Map<CoffeeReadDto>(coffeeRepository.GetCoffeeById(id)));
    }

    [HttpPost]
    public ActionResult<CoffeeReadDto> CreateCoffee(CoffeeCreateDto coffeeCreateDto)
    {
        var coffeeModel = mapper.Map<Coffee>(coffeeCreateDto);
        coffeeRepository.CreateCoffee(coffeeModel);
        coffeeRepository.SaveChanges();

        var coffeeReadDto = mapper.Map<CoffeeReadDto>(coffeeModel);

        //publish newly created coffee to message bus for other services to consume
        var coffeePublishDto = mapper.Map<CoffeePublishDto>(coffeeReadDto);
        coffeePublishDto.Event = "Coffee_Created";
        messageBusClient.PublishCoffee(coffeePublishDto);

        return CreatedAtRoute(nameof(GetCoffeeById), new { coffeeReadDto.Id }, coffeeReadDto);
    }

    [HttpDelete("{id}")]
    public ActionResult RemoveCoffee(int id)
    {
        var coffeeModel = coffeeRepository.GetCoffeeById(id);
        coffeeRepository.RemoveCoffee(coffeeModel);
        coffeeRepository.SaveChanges();

        //publish coffee deletion event to message bus for other services to react
        var coffeePublishDto = mapper.Map<CoffeePublishDto>(mapper.Map<CoffeeReadDto>(coffeeModel));
        coffeePublishDto.Event = "Coffee_Removed";
        messageBusClient.PublishCoffee(coffeePublishDto);

        return Ok(coffeeModel.Id);
    }
}