namespace OrderService.Dtos;

public class CoffeePublishDto : GenericEventDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}