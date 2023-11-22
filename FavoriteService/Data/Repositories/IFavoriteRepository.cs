using FavoriteService.Models;

namespace FavoriteService.Data.Repositories;

public interface IFavoriteRepository
{
    bool SaveChanges();
    Favorite? GetFavoriteByUserIdAndCoffeeId(int userId, int coffeeId);
    IEnumerable<Coffee> GetUserFavoriteCoffees(int userId);
    void AddToUserFavorites(int userId, Coffee coffee);
    void RemoveFromUserFavorites(Favorite favorite);
}