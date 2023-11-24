using System.ComponentModel.DataAnnotations;

namespace CoffeeService.Models;

public class SizeOption
{
    [Key] [Required] public int Id { get; set; }

    [Required] public string? Name { get; set; }

    [Required] public double Price { get; set; }

    [Required] public int CoffeeId { get; set; }

    public Coffee? Coffee { get; set; }
}