using CoffeeService.Models;

namespace CoffeeService.Data.Repositories;

public class SizeOptionRepository(AppDbContext dbContext) : ISizeOptionRepository
{
    public bool SaveChanges()
    {
        return dbContext.SaveChanges() >= 0;
    }

    public void CreateSizeOption(SizeOption sizeOption)
    {
        if (sizeOption == null) throw new ArgumentNullException();

        dbContext.SizeOptions.Add(sizeOption);
    }

    public void RemoveSizeOption(SizeOption sizeOption)
    {
        if (sizeOption == null) throw new ArgumentNullException();

        dbContext.SizeOptions.Remove(sizeOption);
    }

    public SizeOption GetSizeOptionById(int id)
    {
        return dbContext.SizeOptions.FirstOrDefault(sizeOption => sizeOption.Id == id)!;
    }

    public IEnumerable<SizeOption> GetSizeOptionsByCoffeeId(int coffeeId)
    {
        return dbContext.SizeOptions.Where(sizeOption => sizeOption.CoffeeId == coffeeId);
    }
}