using OrderService.Models;

namespace OrderService.SyncDataServices.Grpc;

public interface IGrpcCoffeeDataClient
{
    IEnumerable<Coffee>? ReturnAllCoffees();
}