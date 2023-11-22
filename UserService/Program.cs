using Microsoft.EntityFrameworkCore;
using UserService.AsyncDataServices;
using UserService.Data;
using UserService.Data.Repositories;
using UserService.SyncDataServices.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//connect to database
if (builder.Environment.IsDevelopment())
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("InMem"));

if (builder.Environment.IsProduction())
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySQL(builder.Configuration.GetConnectionString("UserDatabase")!));

// Add services to the container for dependency injection.
builder.Services.AddScoped<IUserRepository, UserRepository>();

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
//add endpoints
app.UseEndpoints(endpoints =>
{
    //add controller endpoints
    endpoints.MapControllers();
    //add grpc endpoints
    endpoints.MapGrpcService<GrpcUserService>();

    //add user.proto file as endpoint for future client to read
    endpoints.MapGet("protos/user.proto",
        async context => { await context.Response.WriteAsync(File.ReadAllText("Protos/user.proto")); });
});

app.Run();