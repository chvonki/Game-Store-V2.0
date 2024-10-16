namespace GameStore.Api.Cors;

public static class CorsExtensions
{
    private const string allowedOriginSetting = "AllowedOrigin";

    public static IServiceCollection AddGameStoreCors(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddCors(options =>
        {
            options.AddDefaultPolicy(corsBuilder =>
            {
                var allowerOrigin = configuration["AllowedOrigin"]
                                    ?? throw new InvalidOperationException("AllowedOrigin is not set");
                corsBuilder.WithOrigins(allowerOrigin)
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .WithExposedHeaders("X-Pagination");
            });
        });
    }
}