using OrderService.Dtos;

namespace OrderService.SyncDataServices.Http;

public interface IHttpCoffeeDataClient
{
    Task<CoffeeHttpGetDto?> GetCoffeeById(int id);
    Task<SizeOptionHttpGetDto?> GetCoffeeSizeOptionById(int coffeeId, int id);
}