var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

builder.Services.AddCarter();

// Configure the HTTP request pipeline.

app.Run();
