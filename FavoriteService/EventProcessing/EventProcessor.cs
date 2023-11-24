using System.Text.Json;
using AutoMapper;
using FavoriteService.Data.Repositories;
using FavoriteService.Dtos;
using FavoriteService.Models;

namespace FavoriteService.EventProcessing;

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
            case EventType.UserCreated:
                AddUser(msg);
                break;
            case EventType.CoffeeCreated:
                AddCoffee(msg);
                break;
            case EventType.CoffeeRemoved:
                RemoveCoffee(msg);
                break;
            case EventType.Unknown:
            default: break;
        }
    }

    private void AddUser(string msg)
    {
        Console.WriteLine("---> Trying to add User");
        using var scope = scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        var userPublishDto = JsonSerializer.Deserialize<UserPublishDto>(msg);

        try
        {
            var userModel = mapper.Map<User>(userPublishDto);

            if (repository.ExternalUserExists(userModel.ExternalId))
                Console.WriteLine($"---> User with {userModel.ExternalId} already exists");

            repository.CreateUser(userModel);
            repository.SaveChanges();
            Console.WriteLine("---> Success");
        }
        catch (Exception e)
        {
            Console.WriteLine($"---> Could not add User to DB: {e.Message}");
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

    private void RemoveCoffee(string msg)
    {
        Console.WriteLine("---> Trying to remove Coffee");
        using var scope = scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<ICoffeeRepository>();
        var coffeePublishDto = JsonSerializer.Deserialize<CoffeePublishDto>(msg);

        try
        {
            var coffeeModel = mapper.Map<Coffee>(coffeePublishDto);

            if (!repository.ExternalCoffeeExists(coffeeModel.ExternalId))
                Console.WriteLine($"---> Coffee with {coffeeModel.ExternalId} does not exist");

            repository.RemoveCoffee(coffeeModel);
            repository.SaveChanges();
            Console.WriteLine("---> Success");
        }
        catch (Exception e)
        {
            Console.WriteLine($"---> Could not remove Coffee from DB: {e.Message}");
        }
    }

    private static EventType DetermineEvent(string msg)
    {
        Console.WriteLine($"---> Determining Event: {msg}");
        var eventType = JsonSerializer.Deserialize<GenericEventDto>(msg);
        Console.WriteLine($"---> Determined Event: {eventType?.Event}");

        return eventType?.Event switch
        {
            "User_Created" => EventType.UserCreated,
            "Coffee_Created" => EventType.CoffeeCreated,
            "Coffee_Removed" => EventType.CoffeeRemoved,
            _ => EventType.Unknown
        };
    }
}

internal enum EventType
{
    UserCreated,
    CoffeeCreated,
    CoffeeRemoved,
    Unknown
}