using System.ComponentModel.DataAnnotations;

namespace OrderService.Models;

public class OrderItem
{
    [Key] [Required] public int Id { get; set; }

    [Required] public int Amount { get; set; }

    [Required] public double Price { get; set; }

    [Required] public GrindType GrindType { get; set; }

    public int SizeOptionId { get; set; }
    public string? SizeOption { get; set; }
    public int CoffeeId { get; set; }
    public Coffee? Coffee { get; set; }
    public int? OrderId { get; set; }
    public Order? Order { get; set; }
}