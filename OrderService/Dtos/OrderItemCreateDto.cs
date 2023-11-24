using OrderService.Models;

namespace OrderService.Dtos;

public class OrderItemCreateDto
{
    public required int CoffeeId { get; set; }
    public required int SizeOptionId { get; set; }
    public required int Amount { get; set; }
    public required GrindType GrindType { get; set; }
}