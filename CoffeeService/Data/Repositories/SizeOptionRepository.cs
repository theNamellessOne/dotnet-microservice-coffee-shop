using CoffeeService.Models;

namespace CoffeeService.Data.Repositories;

public class SizeOptionRepository : ISizeOptionRepository
{
    private readonly AppDbContext _dbContext;

    public SizeOptionRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool SaveChanges()
    {
        return _dbContext.SaveChanges() >= 0;
    }

    public void CreateSizeOption(SizeOption sizeOption)
    {
        if (sizeOption == null) throw new ArgumentNullException();

        _dbContext.SizeOptions.Add(sizeOption);
    }

    public void RemoveSizeOption(SizeOption sizeOption)
    {
        if (sizeOption == null) throw new ArgumentNullException();

        _dbContext.SizeOptions.Remove(sizeOption);
    }

    public SizeOption GetSizeOptionById(int id)
    {
        return _dbContext.SizeOptions.FirstOrDefault(sizeOption => sizeOption.Id == id)!;
    }

    public IEnumerable<SizeOption> GetSizeOptionsByCoffeeId(int coffeeId)
    {
        return _dbContext.SizeOptions.Where(sizeOption => sizeOption.CoffeeId == coffeeId);
    }
}