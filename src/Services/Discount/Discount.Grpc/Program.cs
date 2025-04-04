﻿using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
// Add this line to register the reflection service
builder.Services.AddGrpcReflection();  
builder.Services.AddDbContext<DiscountContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Database"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMigration();

// Accommodates incoming gRPC requests
app.MapGrpcService<DiscountService>();
// Now this line will work because the service is registered
app.MapGrpcReflectionService();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();