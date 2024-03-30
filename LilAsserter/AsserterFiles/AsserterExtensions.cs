namespace LilAsserter.AsserterFiles;
public static class AsserterExtensions
{
    public static IServiceCollection AddAsserter(this IServiceCollection services, AsserterOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        options ??= new();

        services.AddScoped<AsserterService>(serviceProvider =>
        {
            return new AsserterService(options, serviceProvider);
        });

        var serviceProvider = services.BuildServiceProvider();
        var asserterService = serviceProvider.GetRequiredService<AsserterService>();
        Asserter.Initialize(asserterService);

        services.AddScoped<AsserterExceptionFilter>();

        return services;
    }
}
