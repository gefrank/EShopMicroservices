using BuildingBlocks.Behaviors;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System.Reflection;

namespace Ordering.Application
{
    /// <summary>
    /// This is an extension class for setting up dependencies in the Ordering application layer.
    /// This registers services related to the application layer, such as MediatR, Mapster, and other services.
    /// This accessible due to the fact that we have FluentValidation installed in the BuildingBlocks project.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            // Enable feature management for the ordering microservices
            services.AddFeatureManagement();

            // Note this is sending in the assembly information of the current executing assembly
            services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());


            return services;
        }
    }
}
