using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data.Repositories;
using OrderService.Dtos;

namespace OrderService.Controllers;

[Route("api/order/[controller]")]
[ApiController]
public class CoffeeController(
    ICoffeeRepository coffeeRepository,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public ActionResult<CoffeeReadDto> GetAllCoffees()
    {
        return Ok(mapper.Map<IEnumerable<CoffeeReadDto>>(coffeeRepository.GetAllCoffees()));
    }
}