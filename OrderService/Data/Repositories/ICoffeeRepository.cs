using OrderService.Models;

namespace OrderService.Data.Repositories;

public interface ICoffeeRepository
{
    bool SaveChanges();
    void CreateCoffee(Coffee coffee);
    Coffee GetCoffeeByExternalId(int externalId);
    bool ExternalCoffeeExists(int externalCoffeeId);
    bool CoffeeExistsByExternalId(int externalId);
    IEnumerable<Coffee> GetAllCoffees();
}