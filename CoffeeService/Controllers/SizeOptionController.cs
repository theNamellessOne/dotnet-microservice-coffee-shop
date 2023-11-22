using AutoMapper;
using CoffeeService.Data.Repositories;
using CoffeeService.Dtos;
using CoffeeService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeService.Controllers;

[Route("api/coffee/{coffeeId}/[controller]")]
[ApiController]
public class SizeOptionController(ISizeOptionRepository sizeOptionRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<SizeOptionReadDto>> GetCoffeeSizeOptions(int coffeeId)
    {
        return Ok(mapper.Map<IEnumerable<SizeOptionReadDto>>(
            sizeOptionRepository.GetSizeOptionsByCoffeeId(coffeeId)));
    }

    [HttpGet("{sizeOptionId}", Name = "GetCoffeeSizeOptionById")]
    public ActionResult<SizeOptionReadDto> GetCoffeeSizeOptionById(int coffeeId, int sizeOptionId)
    {
        return Ok(mapper.Map<SizeOptionReadDto>(
            sizeOptionRepository.GetSizeOptionById(sizeOptionId)));
    }

    [HttpPost]
    public ActionResult<SizeOptionReadDto> CreateSizeOption(int coffeeId,
        SizeOptionCreateDto sizeOptionCreateDto)
    {
        var sizeOptionModel = mapper.Map<SizeOption>(sizeOptionCreateDto);
        sizeOptionModel.CoffeeId = coffeeId;
        sizeOptionRepository.CreateSizeOption(sizeOptionModel);
        sizeOptionRepository.SaveChanges();

        var sizeOptionsReadDto = mapper.Map<SizeOptionReadDto>(sizeOptionModel);
        return CreatedAtRoute(nameof(GetCoffeeSizeOptionById),
            new { coffeeId, sizeOptionId = sizeOptionsReadDto.Id },
            sizeOptionsReadDto);
    }

    [HttpDelete("{sizeOptionId}")]
    public ActionResult RemoveSizeOption(int sizeOptionId)
    {
        var sizeOptionModel = sizeOptionRepository.GetSizeOptionById(sizeOptionId);
        sizeOptionRepository.RemoveSizeOption(sizeOptionModel);
        sizeOptionRepository.SaveChanges();

        return Ok(sizeOptionModel.Id);
    }
}