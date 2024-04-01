namespace LilAsserter.AsserterFiles;
public static class AsserterExtensions
{
    public static IServiceCollection AddAsserter(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

		services.AddScoped<IAsserter, Asserter>();

        services.AddScoped<AsserterExceptionFilter>();

        return services;
    }
}
