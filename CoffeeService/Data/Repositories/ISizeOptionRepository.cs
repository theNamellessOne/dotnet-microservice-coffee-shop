using CoffeeService.Models;

namespace CoffeeService.Data.Repositories;

public interface ISizeOptionRepository
{
    bool SaveChanges();
    void CreateSizeOption(SizeOption sizeOption);
    void RemoveSizeOption(SizeOption sizeOption);
    SizeOption GetSizeOptionById(int id);
    IEnumerable<SizeOption> GetSizeOptionsByCoffeeId(int coffeeId);
}