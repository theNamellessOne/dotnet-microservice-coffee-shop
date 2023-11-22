using FavoriteService.Models;
using Microsoft.EntityFrameworkCore;

namespace FavoriteService.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    //representation of database table `Users`
    public required DbSet<User> Users { get; set; }

    //representation of database table `Coffees`
    public required DbSet<Coffee> Coffees { get; set; }

    //representation of database table `Favorites`
    public required DbSet<Favorite> Favorites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //creation of manyToOne relation
        //Coffee has many SizeOption
        //SizeOption has one Coffee 
        //SizeOption has foreign key CoffeeId
        modelBuilder.Entity<User>()
            .HasMany(e => e.FavoriteCoffees)
            .WithMany(e => e.FavoredBy)
            .UsingEntity<Favorite>();
    }
}