using AutoMapper;
using FavoriteService.Data.Repositories;
using FavoriteService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteService.Controllers;

[Route("api/favorite/[controller]")]
[ApiController]
public class CoffeeController(
        ICoffeeRepository coffeeRepository,
        IMapper mapper
    )
    : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<CoffeeReadDto>> GetCoffees()
    {
        return Ok(
            mapper.Map<IEnumerable<CoffeeReadDto>>(coffeeRepository.GetAllCoffees())
        );
    }
}