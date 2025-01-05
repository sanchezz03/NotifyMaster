namespace NotifyMaster.WebApi.Extensions;

public static class CorsExtension
{
    public static IServiceCollection AddCORS(this IServiceCollection services)
    {
        return services
            .AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
    }
}
