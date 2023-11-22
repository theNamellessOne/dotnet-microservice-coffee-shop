namespace UserService.Dtos;

public class UserPublishDto
{
    public required int Id { get; set; }
    public required string FullName { get; set; }
    public required string Event { get; set; }
}