using AutoMapper;
using CoffeeService.Data.Repositories;
using Grpc.Core;
using UserService;

namespace CoffeeService.SyncDataServices.Grpc;

//GrpcUserBase is automatically generated based off coffee.proto
public class GrpcCoffeeService(ICoffeeRepository coffeeRepository, IMapper mapper) : GrpcCoffee.GrpcCoffeeBase
{
    //implementation of protobuf method to get all coffees 
    public override Task<CoffeeResponse> GetAllCoffees(GetAllRequest request, ServerCallContext context)
    {
        var coffees = coffeeRepository.GetAllCoffees();
        var response = new CoffeeResponse();

        foreach (var coffee in coffees) response.Coffee.Add(mapper.Map<GrpcCoffeeModel>(coffee));

        return Task.FromResult(response);
    }
}