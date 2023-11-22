namespace FavoriteService.SyncDataServices.Grpc.Coffee;

public interface ICoffeeDataClient
{
    IEnumerable<Models.Coffee>? ReturnAllCoffees();
}