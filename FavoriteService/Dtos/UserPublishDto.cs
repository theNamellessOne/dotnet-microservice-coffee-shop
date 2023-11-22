namespace FavoriteService.Dtos;

public class UserPublishDto : GenericEventDto
{
    public required int Id { get; set; }
    public required string FullName { get; set; }
}