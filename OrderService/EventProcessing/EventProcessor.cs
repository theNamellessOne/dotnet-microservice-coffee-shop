using System.Text.Json;
using AutoMapper;
using OrderService.Data.Repositories;
using OrderService.Dtos;
using OrderService.Models;

namespace OrderService.EventProcessing;

public class EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper) : IEventProcessor
{
    //since event processor is a singleton service
    //it is impossible to inject scoped repositories directly
    //so it is necessary to create them using scope factory
    public void ProcessEvent(string msg)
    {
        Console.WriteLine("---> Processing Event");
        switch (DetermineEvent(msg))
        {
            case EventType.CoffeeCreated:
                AddCoffee(msg);
                break;
            case EventType.Unknown:
            default: break;
        }
    }

    private void AddCoffee(string msg)
    {
        Console.WriteLine("---> Trying to add Coffee");
        using var scope = scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<ICoffeeRepository>();
        var coffeePublishDto = JsonSerializer.Deserialize<CoffeePublishDto>(msg);

        try
        {
            var coffeeModel = mapper.Map<Coffee>(coffeePublishDto);

            if (repository.ExternalCoffeeExists(coffeeModel.ExternalId))
                Console.WriteLine($"---> Coffee with {coffeeModel.ExternalId} already exists");

            repository.CreateCoffee(coffeeModel);
            repository.SaveChanges();
            Console.WriteLine("---> Success");
        }
        catch (Exception e)
        {
            Console.WriteLine($"---> Could not add Coffee to DB: {e.Message}");
        }
    }

    private static EventType DetermineEvent(string msg)
    {
        Console.WriteLine($"---> Determining Event: {msg}");
        var eventType = JsonSerializer.Deserialize<GenericEventDto>(msg);
        Console.WriteLine($"---> Determined Event: {eventType?.Event}");

        return eventType?.Event switch
        {
            "Coffee_Created" => EventType.CoffeeCreated,
            _ => EventType.Unknown
        };
    }
}

internal enum EventType
{
    CoffeeCreated,
    Unknown
}