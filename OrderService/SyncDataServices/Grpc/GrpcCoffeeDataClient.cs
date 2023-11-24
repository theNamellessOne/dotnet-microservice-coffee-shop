using AutoMapper;
using CoffeeService;
using Grpc.Net.Client;
using OrderService.Models;

namespace OrderService.SyncDataServices.Grpc;

public class GrpcCoffeeDataClient(IConfiguration configuration, IMapper mapper) : IGrpcCoffeeDataClient
{
    public IEnumerable<Coffee>? ReturnAllCoffees()
    {
        Console.WriteLine($"---> Calling GRPC User Service {configuration["GrpcCoffee"]}");
        var channel = GrpcChannel.ForAddress(configuration["GrpcCoffee"]!);
        var client = new GrpcCoffee.GrpcCoffeeClient(channel);
        var request = new GetAllRequest();

        try
        {
            var response = client.GetAllCoffees(request);
            return mapper.Map<IEnumerable<Coffee>>(response.Coffee);
        }
        catch (Exception e)
        {
            Console.WriteLine($"---> Could not call GRPC Coffee Service {configuration["GrpcCoffee"]}");
            Console.WriteLine($"---> Error {e.Message}");
            return null;
        }
    }
}