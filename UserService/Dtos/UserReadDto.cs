namespace UserService.Dtos;

public class UserReadDto
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? FullName { get; set; }
}