using CoffeeService.Models;

namespace CoffeeService.Data.Repositories;

public class CoffeeRepository : ICoffeeRepository
{
    private readonly AppDbContext _dbContext;

    public CoffeeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool SaveChanges()
    {
        return _dbContext.SaveChanges() >= 0;
    }

    public void CreateCoffee(Coffee coffee)
    {
        if (coffee == null) throw new ArgumentNullException();

        _dbContext.Coffees.Add(coffee);
    }

    public void RemoveCoffee(Coffee coffee)
    {
        if (coffee == null) throw new ArgumentNullException();

        _dbContext.Coffees.Remove(coffee);
    }

    public Coffee GetCoffeeById(int id)
    {
        return _dbContext.Coffees.FirstOrDefault(coffee => coffee.Id == id)!;
    }

    public IEnumerable<Coffee> GetAllCoffees()
    {
        return _dbContext.Coffees.ToList();
    }
}