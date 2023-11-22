using System.ComponentModel.DataAnnotations;

namespace CoffeeService.Models;

public class Coffee
{
    [Key] [Required] public int Id { get; set; }

    [Required] public string? Name { get; set; }

    [Required] public CoffeeRating? Strength { get; set; }

    [Required] public CoffeeRating? Flavour { get; set; }

    [Required] public CoffeeRating? Aroma { get; set; }

    public ICollection<SizeOption>? SizeOptions { get; set; }
}