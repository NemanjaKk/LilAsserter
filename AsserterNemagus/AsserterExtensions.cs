using Microsoft.Extensions.DependencyInjection;
using System;

namespace LilAsserter.AsserterNemagus
{
    /// <summary>
    /// Extension methods for adding Asserter services to the DI container.
    /// </summary>
    public static class AsserterExtensions
    {
        /// <summary>
        /// Adds Asserter services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="asserterOptions">Optional configuration for the Asserter service.</param>
        /// <returns>The <paramref name="serviceCollection"/> with added Asserter services.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="serviceCollection"/> is null.</exception>
        public static IServiceCollection AddAsserter(this IServiceCollection serviceCollection, AsserterOptions? asserterOptions = null)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }
            asserterOptions ??= new AsserterOptions();

            serviceCollection.Configure<AsserterOptions>(options =>
            {
                options.EnableLogging = asserterOptions.EnableLogging;
            });
            serviceCollection.AddScoped<IAsserter, Asserter>();
            serviceCollection.AddScoped<AsserterExceptionFilter>();

            return serviceCollection;
        }
    }
}