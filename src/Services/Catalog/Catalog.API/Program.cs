var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    // Register all the handlers in the assembly, a handler is a class that implements IRequestHandler
    // Tells mediatR where to find our commands and queries classes
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);  
});
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!); 
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();
