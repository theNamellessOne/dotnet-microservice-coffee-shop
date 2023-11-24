using System.ComponentModel.DataAnnotations;

namespace OrderService.Models;

public class Coffee
{
    [Key] [Required] public int Id { get; set; }

    [Required] public int ExternalId { get; set; }

    [Required] public string? Name { get; set; }
}