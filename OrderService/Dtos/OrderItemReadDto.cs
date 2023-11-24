using OrderService.Models;

namespace OrderService.Dtos;

public class OrderItemReadDto
{
    public required int Id { get; set; }
    public required int Amount { get; set; }
    public required double Price { get; set; }
    public required string SizeOption { get; set; }
    public required GrindType GrindType { get; set; }
    public required int CoffeeId { get; set; }
    public required Coffee Coffee { get; set; }
    public required int OrderId { get; set; }
}