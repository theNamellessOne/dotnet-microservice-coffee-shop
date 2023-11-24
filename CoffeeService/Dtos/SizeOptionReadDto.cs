namespace CoffeeService.Dtos;

public class SizeOptionReadDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public double Price { get; set; }
    public int CoffeeId { get; set; }
}