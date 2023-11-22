using CoffeeService.AsyncDataServices;
using CoffeeService.Data;
using CoffeeService.Data.Repositories;
using CoffeeService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//select database based on environment
if (builder.Environment.IsDevelopment())
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("InMem"));

if (builder.Environment.IsProduction())
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySQL(builder.Configuration.GetConnectionString("CoffeeDatabase")!));

// Add services to the container for dependency injection.
builder.Services.AddScoped<ICoffeeRepository, CoffeeRepository>();
builder.Services.AddScoped<ISizeOptionRepository, SizeOptionRepository>();

//Add message bus for async data services as Singleton
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

//Add Grpc service
builder.Services.AddGrpc();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

var app = builder.Build();

//prepare database
PrepareDb.PreparePopulation(app, app.Environment);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    //add grpc endpoints
    endpoints.MapGrpcService<GrpcCoffeeService>();

    //add user.proto file as endpoint for future client to read
    endpoints.MapGet("protos/coffee.proto",
        async context => { await context.Response.WriteAsync(File.ReadAllText("Protos/coffee.proto")); });
});

app.Run();