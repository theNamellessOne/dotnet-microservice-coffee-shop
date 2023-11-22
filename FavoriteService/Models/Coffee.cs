using System.ComponentModel.DataAnnotations;

namespace FavoriteService.Models;

public class Coffee
{
    [Key] [Required] public int Id { get; set; }

    [Required] public int ExternalId { get; set; }

    [Required] public string? Name { get; set; }

    public ICollection<User>? FavoredBy { get; set; }
}