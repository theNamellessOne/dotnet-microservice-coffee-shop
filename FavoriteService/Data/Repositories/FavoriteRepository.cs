using FavoriteService.Models;
using Microsoft.EntityFrameworkCore;

namespace FavoriteService.Data.Repositories;

public class FavoriteRepository(AppDbContext dbContext) : IFavoriteRepository
{
    public bool SaveChanges()
    {
        return dbContext.SaveChanges() >= 0;
    }

    public Favorite? GetFavoriteByUserIdAndCoffeeId(int userId, int coffeeId)
    {
        return dbContext.Favorites.FirstOrDefault(favorite =>
            favorite.UserId == userId && favorite.CoffeeId == coffeeId);
    }

    public IEnumerable<Coffee> GetUserFavoriteCoffees(int userId)
    {
        List<Coffee> userFavoriteCoffees = new();
        var userFavorites = dbContext.Favorites.Where(favorite => favorite.UserId == userId)
            .Include(favorite => favorite.Coffee).ToList();

        foreach (var favorite in userFavorites)
            if (favorite.Coffee != null)
                userFavoriteCoffees.Add(favorite.Coffee);

        return userFavoriteCoffees;
    }

    public void AddToUserFavorites(int userId, Coffee coffee)
    {
        dbContext.Favorites.Add(new Favorite
        {
            UserId = userId,
            CoffeeId = coffee.Id
        });
    }

    public void RemoveFromUserFavorites(Favorite favorite)
    {
        dbContext.Favorites.Remove(favorite);
    }
}