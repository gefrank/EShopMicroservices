// CQRS (Command Query Responsibility Segregation) is an architectural pattern that separates an application's operations into two distinct categories:
// 1.	Commands 
// 2.   Queries 
// This project implements CQRS using MediatR and Marten:

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    // Register all the handlers in the assembly, a handler is a class that implements IRequestHandler
    // Tells mediatR where to find our commands and queries classes
    config.RegisterServicesFromAssembly(assembly); 
    config.AddOpenBehavior(typeof(ValidationBehavior<,>)); // Register the validation behavior
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
// Register all the validators in the assembly, a validator is a class that implements IValidator<T>
// scans the project for any validators and registers them with the DI container
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

// Marten simplifies data access by eliminating the need for explicit ORM mappings while leveraging
// PostgreSQL's JSON capabilities for document storage, making it well-suited for this .NET 8
// application using a CQRS pattern with MediatR.

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!); 
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>(); // Seed the database with initial data
}

// Register a custom exception handler for handling exceptions globally, from building blocks
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks() // Add health checks for monitoring the application's health
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!); // Check the PostgreSQL database connection
    
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

// Use the custom exception handler, options are defined in the CustomExceptionHandler class
app.UseExceptionHandler(options => { }); 

app.UseHealthChecks("/health", // Map health checks to the /health endpoint
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse // Use the UI response writer for health checks
    }); 

app.Run();
