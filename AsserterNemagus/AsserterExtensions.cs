using Microsoft.Extensions.DependencyInjection;
using System;

namespace LilAsserter.AsserterNemagus
{
    public static class AsserterExtensions
    {
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