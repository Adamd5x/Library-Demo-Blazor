namespace Library.Api.Extensions.DependencyInjection
{
    public static class CorsExtension
    {
        private const string CorsPolicyName = "OpenPolicy";
        public static IServiceCollection AddCorsServices (this IServiceCollection services)
        {

            services.AddCors (setup => {
                setup.AddPolicy (CorsPolicyName, builder => {
                    builder.AllowAnyOrigin ();
                    builder.AllowAnyHeader ();
                    builder.AllowAnyMethod ();
                });
            });
            return services;
        }

        public static IApplicationBuilder UseCorsServices (this IApplicationBuilder app)
        {
            app.UseCors (CorsPolicyName);
            return app;
        }
    }
}
