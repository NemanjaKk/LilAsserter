using Microsoft.Extensions.DependencyInjection;
using System;

namespace LilAsserter.AsserterNemagus
{
    public static class AsserterExtensions
    {
        public static IServiceCollection AddAsserter(this IServiceCollection serviceCollection)
        {
			if (serviceCollection is null)
			{
				throw new ArgumentNullException(nameof(serviceCollection));
			}

			serviceCollection.AddScoped<IAsserter, Asserter>();

			serviceCollection.AddScoped<AsserterExceptionFilter>();

            return serviceCollection;
        }
    }
}