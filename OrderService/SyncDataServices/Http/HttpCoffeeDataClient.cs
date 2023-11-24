using OrderService.Dtos;

namespace OrderService.SyncDataServices.Http;

public class HttpCoffeeDataClient : IHttpCoffeeDataClient
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public HttpCoffeeDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;

        _httpClient.Timeout = TimeSpan.FromSeconds(5);
    }

    public async Task<CoffeeHttpGetDto?> GetCoffeeById(int id)
    {
        return await Get($"{_configuration["CoffeeService"]}/api/coffee/{id}").Result
            .ReadFromJsonAsync<CoffeeHttpGetDto>();
    }

    public async Task<SizeOptionHttpGetDto?> GetCoffeeSizeOptionById(int coffeeId, int id)
    {
        return await Get($"{_configuration["CoffeeService"]}/api/coffee/{coffeeId}/sizeOption/{id}").Result
            .ReadFromJsonAsync<SizeOptionHttpGetDto>();
    }

    private async Task<HttpContent> Get(string requestUrl)
    {
        Console.WriteLine($"---> Sending Get Request to {requestUrl}");

        var response = await _httpClient.GetAsync(requestUrl);

        Console.WriteLine(response.IsSuccessStatusCode
            ? "---> Sync GET to Service was OK"
            : "---> Sync GET to Service wa NOT OK");

        return response.Content;
    }
}