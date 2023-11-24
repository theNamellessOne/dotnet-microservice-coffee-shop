using CoffeeService.Models;

namespace CoffeeService.Dtos;

public class CoffeeReadDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required CoffeeRating Strength { get; set; }
    public required CoffeeRating Flavour { get; set; }
    public required CoffeeRating Aroma { get; set; }
    public ICollection<SizeOptionReadDto>? SizeOptions { get; set; }
}