using System.ComponentModel.DataAnnotations;

namespace FavoriteService.Models;

public class User
{
    [Key] [Required] public int Id { get; set; }

    [Required] public int ExternalId { get; set; }

    [Required] public string? FullName { get; set; }

    public ICollection<Coffee>? FavoriteCoffees { get; set; }
}