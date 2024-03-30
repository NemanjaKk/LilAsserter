namespace LilAsserter.AsserterFiles
{
    public class AsserterInitializationService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public AsserterInitializationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<AsserterInitializer>();
                initializer.Initialize();
            }

            await Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
