using CoffeeService.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeService.Data;

public static class PrepareDb
{
    public static void PreparePopulation(IApplicationBuilder applicationBuilder, IHostEnvironment environment)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>()!;

        //fill database with test data
        if (environment.IsDevelopment())
            Seed(dbContext);

        //run database migrations
        if (environment.IsProduction())
            Migrate(dbContext);
    }

    private static void Migrate(AppDbContext dbContext)
    {
        Console.WriteLine("---> Migrating");
        dbContext.Database.Migrate();
    }


    private static void Seed(AppDbContext dbContext)
    {
        if (dbContext.Coffees.Any()) return;
        if (dbContext.SizeOptions.Any()) return;

        SeedCoffees(dbContext);
        SeedSizeOptionsForCoffee(dbContext, 1);
        SeedSizeOptionsForCoffee(dbContext, 2);
        SeedSizeOptionsForCoffee(dbContext, 3);
    }

    private static void SeedCoffees(AppDbContext dbContext)
    {
        dbContext.Coffees.AddRange(
            new Coffee
            {
                Name = "Coffee 1",
                Strength = CoffeeRating.Fine,
                Flavour = CoffeeRating.Awesome,
                Aroma = CoffeeRating.Great
            },
            new Coffee
            {
                Name = "Coffee 2",
                Strength = CoffeeRating.Awesome,
                Flavour = CoffeeRating.Okay,
                Aroma = CoffeeRating.Awesome
            },
            new Coffee
            {
                Name = "Coffee 3",
                Strength = CoffeeRating.Good,
                Flavour = CoffeeRating.Good,
                Aroma = CoffeeRating.Awesome
            }
        );

        dbContext.SaveChanges();
    }

    private static void SeedSizeOptionsForCoffee(AppDbContext dbContext, int coffeeId)
    {
        dbContext.SizeOptions.AddRange(
            new SizeOption
            {
                Name = "250g",
                Price = 249.50,
                CoffeeId = coffeeId
            },
            new SizeOption
            {
                Name = "500g",
                Price = 499.50,
                CoffeeId = coffeeId
            },
            new SizeOption
            {
                Name = "1000g",
                Price = 999.50,
                CoffeeId = coffeeId
            }
        );

        dbContext.SaveChanges();
    }
}