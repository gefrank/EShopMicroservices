namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            // services.AddCarter(); // Uncomment if using Carter for minimal APIs

            return services;
        }

        public static WebApplication UserApiServices(this WebApplication app)
        {
            // Uncomment if using Carter for minimal APIs
            // app.MapCarter();
            return app;
        }
    }
}
