namespace FavoriteService.Models;

public class Favorite
{
    public int UserId { get; set; }
    public int CoffeeId { get; set; }
    public Coffee? Coffee { get; set; }
}