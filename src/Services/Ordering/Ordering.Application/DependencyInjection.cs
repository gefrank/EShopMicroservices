using BuildingBlocks.Behaviors;
using Microsoft.Extensions.DependencyInjection;
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
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                //config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                //config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });
            return services;
        }
    }
}
