
using BuildingBlocks.Exceptions.Handler;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Application Services
var assembly = typeof(Program).Assembly;
// Carter simplifies the building of api endpoints
// scans the assembly for CarterModule classes and exposes minimal APIs
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// Data Services
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
// Note using Scrutor library to decorate the IBasketRepository with the CachedBasketRepository
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
// Allows us to inject the IDistributedCache into the CachedBasketRepository
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis")!;
});

//gRPC Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
})
.ConfigurePrimaryHttpMessageHandler(() => // Allow self-signed certificates for development purposes
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

    return handler;
});


// Cross-Cutting Services
// Register a custom exception handler for handling exceptions globally, from building blocks
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Add health checks for monitoring the application's health
builder.Services.AddHealthChecks() 
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!); 

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

// Use the custom exception handler, options are defined in the CustomExceptionHandler class
app.UseExceptionHandler(options => { });

// Map health checks to the /health endpoint
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse // Use the UI response writer for health checks
    });

app.Run();
