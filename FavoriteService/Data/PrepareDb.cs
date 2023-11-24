using FavoriteService.Data.Repositories;
using FavoriteService.Models;
using FavoriteService.SyncDataServices.Grpc.Coffee;
using FavoriteService.SyncDataServices.Grpc.User;
using Microsoft.EntityFrameworkCore;

namespace FavoriteService.Data;

public static class PrepareDb
{
    public static void PreparePopulation(IApplicationBuilder applicationBuilder, IHostEnvironment environment)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>()!;

        //fill database with test data
        if (environment.IsDevelopment())
            Seed(dbContext);

        //run database migrations and pull database entities from UserService and CoffeeService
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
        PullUsers(serviceScope);
        PullCoffees(serviceScope);
    }

    private static void PullCoffees(IServiceScope serviceScope)
    {
        var coffeeRepository = serviceScope.ServiceProvider.GetService<ICoffeeRepository>()!;
        var coffeeGrpcClient = serviceScope.ServiceProvider.GetService<ICoffeeDataClient>();
        var coffees = coffeeGrpcClient!.ReturnAllCoffees();

        foreach (var coffee in coffees!)
        {
            if (coffeeRepository.ExternalCoffeeExists(coffee.ExternalId)) continue;

            coffeeRepository.CreateCoffee(coffee);
        }

        coffeeRepository.SaveChanges();
    }

    private static void PullUsers(IServiceScope serviceScope)
    {
        var userRepository = serviceScope.ServiceProvider.GetService<IUserRepository>()!;
        var userGrpcClient = serviceScope.ServiceProvider.GetService<IUserDataClient>();
        var users = userGrpcClient!.ReturnAllUsers();

        foreach (var user in users!)
        {
            if (userRepository.ExternalUserExists(user.ExternalId)) continue;

            userRepository.CreateUser(user);
        }

        userRepository.SaveChanges();
    }

    private static void Seed(AppDbContext dbContext)
    {
        if (dbContext.Users.Any()) return;
        if (dbContext.Coffees.Any()) return;
        if (dbContext.Favorites.Any()) return;

        Console.WriteLine("---> Seeding");
        SeedUsers(dbContext);
        SeedCoffees(dbContext);
        SeedFavorites(dbContext);
    }

    private static void SeedUsers(AppDbContext dbContext)
    {
        dbContext.Users.AddRange(
            new User
            {
                FullName = "Oleksandr Oleksandrovych Oleksandriuck",
                ExternalId = 1
            },
            new User
            {
                FullName = "Ihor Oleksandrovych Oleksandriuck",
                ExternalId = 2
            },
            new User
            {
                FullName = "Nazar Oleksandrovych Oleksandriuck",
                ExternalId = 3
            });
        dbContext.SaveChanges();
    }

    private static void SeedCoffees(AppDbContext dbContext)
    {
        dbContext.Coffees.AddRange(
            new Coffee
            {
                Name = "Coffee 1",
                ExternalId = 1
            },
            new Coffee
            {
                Name = "Coffee 2",
                ExternalId = 2
            },
            new Coffee
            {
                Name = "Coffee 3",
                ExternalId = 3
            }
        );
        dbContext.SaveChanges();
    }

    private static void SeedFavorites(AppDbContext dbContext)
    {
        dbContext.Favorites.AddRange(
            new Favorite
            {
                CoffeeId = 1,
                UserId = 1
            },
            new Favorite
            {
                CoffeeId = 2,
                UserId = 1
            },
            new Favorite
            {
                CoffeeId = 3,
                UserId = 2
            },
            new Favorite
            {
                CoffeeId = 2,
                UserId = 2
            },
            new Favorite
            {
                CoffeeId = 1,
                UserId = 3
            }
        );
        dbContext.SaveChanges();
    }
}