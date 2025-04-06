using Carter;

namespace Ordering.API
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Extension method to add API services to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            services.AddCarter(); 

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

            return app;
        }
    }
}
