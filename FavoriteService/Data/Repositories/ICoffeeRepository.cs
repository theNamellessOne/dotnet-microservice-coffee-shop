using FavoriteService.Models;

namespace FavoriteService.Data.Repositories;

public interface ICoffeeRepository
{
    bool SaveChanges();
    void CreateCoffee(Coffee coffee);
    void RemoveCoffee(Coffee coffee);
    bool ExternalCoffeeExists(int externalCoffeeId);
    Coffee? GetCoffeeById(int externalId);
    IEnumerable<Coffee> GetAllCoffees();
}