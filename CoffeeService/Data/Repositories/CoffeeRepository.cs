using CoffeeService.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeService.Data.Repositories;

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

    public void RemoveCoffee(Coffee coffee)
    {
        if (coffee == null) throw new ArgumentNullException();

        dbContext.Coffees.Remove(coffee);
    }

    public Coffee GetCoffeeById(int id)
    {
        return dbContext.Coffees.Include(coffee => coffee.SizeOptions).FirstOrDefault(coffee => coffee.Id == id)!;
    }

    public IEnumerable<Coffee> GetAllCoffees()
    {
        return dbContext.Coffees.Include(coffee => coffee.SizeOptions).ToList();
    }
}