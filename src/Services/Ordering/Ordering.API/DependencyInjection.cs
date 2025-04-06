using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ordering.API
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Extension method to add API services to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCarter(); 

            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddHealthChecks().AddSqlServer(configuration.GetConnectionString("Database")!);

            return services;
        }

        /// <summary>
        /// Extension method to add API services to the WebApplication.
        /// Allows for the mapping of Carter minimal api endpoints.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static WebApplication UseApiServices(this WebApplication app)
        {       
            app.MapCarter();

            // Setup exception handling middleware, configures the app to use a custom exception handler in the pipeline.
            app.UseExceptionHandler(options => { });

            // Setup health checks middleware, configures the app to use health checks in the pipeline.
            app.UseHealthChecks("/health",
                new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

            return app;
        }
    }
}
