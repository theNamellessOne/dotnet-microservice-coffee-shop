using OrderService.Models;

namespace OrderService.Data.Repositories;

public class CoffeeRepository(AppDbContext dbContext) : ICoffeeRepository
{
    public bool SaveChanges()
    {
        return dbContext.SaveChanges() >= 0;
    }

    public void CreateCoffee(Coffee coffee)
    {
        if (coffee == null) throw new ArgumentNullException();
        dbContext.Coffees.Add(coffee);
    }

    public bool ExternalCoffeeExists(int externalCoffeeId)
    {
        return dbContext.Coffees.Any(coffee => coffee.ExternalId == externalCoffeeId);
    }

    public bool CoffeeExistsByExternalId(int externalId)
    {
        return dbContext.Coffees.Any(coffee => coffee.ExternalId == externalId);
    }

    public IEnumerable<Coffee> GetAllCoffees()
    {
        return dbContext.Coffees.ToList();
    }

    public Coffee GetCoffeeByExternalId(int externalId)
    {
        return dbContext.Coffees.FirstOrDefault(coffee => coffee.ExternalId == externalId)!;
    }
}