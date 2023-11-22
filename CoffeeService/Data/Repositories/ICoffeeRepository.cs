using CoffeeService.Models;

namespace CoffeeService.Data.Repositories;

public interface ICoffeeRepository
{
    bool SaveChanges();
    void CreateCoffee(Coffee coffee);
    void RemoveCoffee(Coffee coffee);
    Coffee GetCoffeeById(int id);
    IEnumerable<Coffee> GetAllCoffees();
}