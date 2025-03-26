
using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

// Register a custom exception handler for handling exceptions globally, from building blocks
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

// Use the custom exception handler, options are defined in the CustomExceptionHandler class
app.UseExceptionHandler(options => { });

app.Run();
