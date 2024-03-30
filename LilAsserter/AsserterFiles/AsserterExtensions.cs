namespace LilAsserter.AsserterFiles;
public static class AsserterExtensions
{
    public static IServiceCollection AddAsserter(this IServiceCollection serviceCollection, AsserterOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(serviceCollection);

        options ??= new();

        serviceCollection.AddScoped<AsserterService>(serviceProvider =>
        {
            return new AsserterService(options, serviceProvider);
        });
        serviceCollection.AddScoped<AsserterInitializer>();
        serviceCollection.AddHostedService<AsserterInitializationService>();

        return serviceCollection;
    }
}
