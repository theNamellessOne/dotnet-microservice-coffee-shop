using System.ComponentModel.DataAnnotations;

namespace UserService.Dtos;

public class UserCreateDto
{
    [Required] public string? Email { get; set; }

    [Required] public string? FullName { get; set; }

    [Required] public string? Password { get; set; }
}