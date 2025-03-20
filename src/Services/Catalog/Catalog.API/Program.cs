using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    // Register all the handlers in the assembly, a handler is a class that implements IRequestHandler
    // Tells mediatR where to find our commands and queries classes
    config.RegisterServicesFromAssembly(assembly); 
    config.AddOpenBehavior(typeof(ValidationBehavior<,>)); // Register the validation behavior
});
// Register all the validators in the assembly, a validator is a class that implements IValidator<T>
// scans the project for any validators and registers them with the DI container
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!); 
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();
