using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Data;

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
        if (dbContext.Users.Any()) return;

        dbContext.Users.AddRange(
            new User
            {
                FullName = "Oleksandr Oleksandrovych Oleksandriuck",
                Email = "oleksandr@gmail.com",
                Password = "oleksandr"
            },
            new User
            {
                FullName = "Ihor Oleksandrovych Oleksandriuck",
                Email = "ihor@gmail.com",
                Password = "ihor"
            },
            new User
            {
                FullName = "Nazar Oleksandrovych Oleksandriuck",
                Email = "nazar@gmail.com",
                Password = "nazar"
            });
        dbContext.SaveChanges();
    }
}