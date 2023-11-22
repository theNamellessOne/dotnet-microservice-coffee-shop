using AutoMapper;
using CoffeeService;
using Grpc.Net.Client;

namespace FavoriteService.SyncDataServices.Grpc.Coffee;

public class CoffeeDataClient(IConfiguration configuration, IMapper mapper) : ICoffeeDataClient
{
    public IEnumerable<Models.Coffee>? ReturnAllCoffees()
    {
        Console.WriteLine($"---> Calling GRPC User Service {configuration["GrpcCoffee"]}");
        var channel = GrpcChannel.ForAddress(configuration["GrpcCoffee"]!);
        var client = new GrpcCoffee.GrpcCoffeeClient(channel);
        var request = new GetAllRequest();

        try
        {
            var response = client.GetAllCoffees(request);
            return mapper.Map<IEnumerable<Models.Coffee>>(response.Coffee);
        }
        catch (Exception e)
        {
            Console.WriteLine($"---> Could not call GRPC Coffee Service {configuration["GrpcCoffee"]}");
            Console.WriteLine($"---> Error {e.Message}");
            return null;
        }
    }
}