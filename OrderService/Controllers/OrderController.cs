using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data.Repositories;
using OrderService.Dtos;
using OrderService.Models;
using OrderService.SyncDataServices.Http;

namespace OrderService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(
    IHttpCoffeeDataClient httpCoffeeDataClient,
    ICoffeeRepository coffeeRepository,
    IOrderRepository orderRepository,
    IMapper mapper
) : ControllerBase
{
    [HttpGet]
    public ActionResult<OrderReadDto> GetAllOrders()
    {
        return Ok(mapper.Map<IEnumerable<OrderReadDto>>(orderRepository.GetAllOrders()));
    }

    [HttpGet("{id}", Name = "GetOrderById")]
    public ActionResult<OrderReadDto> GetOrderById(int id)
    {
        return Ok(mapper.Map<OrderReadDto>(orderRepository.GetOrderById(id)));
    }

    [HttpPost("PlaceOrder")]
    public async Task<ActionResult<OrderReadDto>> PlaceOrder(OrderCreateDto createDto)
    {
        //create order
        var orderModel = mapper.Map<Order>(createDto);

        //initialize orderItems and calculate total
        var total = 0.0;
        var items = orderModel.OrderItems.ToList();
        for (var i = 0; i < items.Count; i++)
        {
            var item = items[i];

            //try to get data from CoffeeService and fill it into OrderItems
            try
            {
                var coffee = await httpCoffeeDataClient.GetCoffeeById(item.CoffeeId);

                if (coffee == null) return BadRequest();

                item.CoffeeId = coffee.Id;

                if (!coffeeRepository.CoffeeExistsByExternalId(coffee.Id))
                {
                    var coffeeModel = mapper.Map<Coffee>(coffee);
                    coffeeModel.Id = 0;
                    coffeeModel.ExternalId = coffee.Id;
                    coffeeRepository.CreateCoffee(coffeeModel);
                    coffeeRepository.SaveChanges();
                }

                var sizeOption = await httpCoffeeDataClient.GetCoffeeSizeOptionById(coffee.Id, item.SizeOptionId);

                if (sizeOption == null) return BadRequest();

                item.SizeOption = sizeOption.Name;
                item.Price = sizeOption.Price * item.Amount;

                total += item.Price;
            }
            catch (Exception e)
            {
                Console.WriteLine($"---> Could Not Get Data From Coffee Service: {e}");
                return BadRequest();
            }
        }

        orderModel.OrderItems = items;
        orderModel.Total = total;
        orderRepository.CreateOrder(orderModel);
        if (!orderRepository.SaveChanges()) return BadRequest();

        return CreatedAtRoute(nameof(GetOrderById), new { orderModel.Id }, mapper.Map<OrderReadDto>(orderModel));
    }
}