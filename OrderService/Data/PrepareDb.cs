using Microsoft.EntityFrameworkCore;
using OrderService.Data.Repositories;
using OrderService.SyncDataServices.Grpc;

namespace OrderService.Data;

public static class PrepareDb
{
    public static void PreparePopulation(IApplicationBuilder applicationBuilder, IHostEnvironment environment)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>()!;

        //run database migrations and pull database entities from CoffeeService
        if (environment.IsProduction())
        {
            Migrate(dbContext);
            Pull(serviceScope);
        }
    }

    private static void Migrate(AppDbContext dbContext)
    {
        Console.WriteLine("---> Migrating");
        dbContext.Database.Migrate();
    }

    private static void Pull(IServiceScope serviceScope)
    {
        var coffeeRepository = serviceScope.ServiceProvider.GetService<ICoffeeRepository>();
        var coffeeDataClient = serviceScope.ServiceProvider.GetService<IGrpcCoffeeDataClient>();
        var coffees = coffeeDataClient!.ReturnAllCoffees();

        foreach (var coffee in coffees!)
        {
            Console.Write(coffee.Id);
            Console.Write(";");
            Console.Write(coffee.ExternalId);
            Console.Write(";");
            Console.Write(coffee.Name);
            Console.WriteLine(";");
            if (coffeeRepository!.ExternalCoffeeExists(coffee.ExternalId)) continue;

            coffeeRepository.CreateCoffee(coffee);
        }

        coffeeRepository!.SaveChanges();
    }
}