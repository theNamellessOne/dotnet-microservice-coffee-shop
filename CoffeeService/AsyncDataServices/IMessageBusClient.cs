using CoffeeService.Dtos;

namespace CoffeeService.AsyncDataServices;

public interface IMessageBusClient
{
    void PublishCoffee(CoffeePublishDto coffeePublishDto);
}