using System.ComponentModel.DataAnnotations;

namespace CoffeeService.Dtos;

public class SizeOptionCreateDto
{
    [Required] public string Name { get; set; }

    [Required] public double Price { get; set; }
}