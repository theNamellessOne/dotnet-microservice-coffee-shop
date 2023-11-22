using System.ComponentModel.DataAnnotations;
using CoffeeService.Models;

namespace CoffeeService.Dtos;

public class CoffeeCreateDto
{
    [Required] public string Name { get; set; }

    [Required] public CoffeeRating Strength { get; set; }

    [Required] public CoffeeRating Flavour { get; set; }

    [Required] public CoffeeRating Aroma { get; set; }
}