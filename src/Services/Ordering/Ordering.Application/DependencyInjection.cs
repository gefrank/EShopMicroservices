using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application
{
    /// <summary>
    /// This is an extension class for setting up dependencies in the Ordering application layer.
    /// This registers services related to the application layer, such as MediatR, Mapster, and other services.
    /// This accessible due to the fact that we have FluentValidation installed in the BuildingBlocks project.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register application services here
            // For example, you can add MediatR, AutoMapper, etc.
            // Example:
            // services.AddMediatR(typeof(DependencyInjection).Assembly);
            // services.AddAutoMapper(typeof(DependencyInjection).Assembly);
            return services;
        }
    }
}
