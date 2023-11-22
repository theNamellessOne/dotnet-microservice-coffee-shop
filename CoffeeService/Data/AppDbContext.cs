using CoffeeService.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeService.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    //representation of database table `Coffees`
    public required DbSet<Coffee> Coffees { get; set; }

    //representation of database table `SizeOptions`
    public required DbSet<SizeOption> SizeOptions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //creation of manyToMany relation
        //User has many FavoriteCoffees
        //Coffee is Favored By many User
        //Favorite is a table representation of manyToMany with ids
        modelBuilder.Entity<Coffee>()
            .HasMany(e => e.SizeOptions)
            .WithOne(e => e.Coffee)
            .HasForeignKey(e => e.CoffeeId)
            .IsRequired();
    }
}