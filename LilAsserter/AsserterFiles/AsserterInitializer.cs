namespace LilAsserter.AsserterFiles
{
    public class AsserterInitializer
    {
        private readonly IServiceProvider _serviceProvider;
        private bool _initialized;

        public AsserterInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Initialize()
        {
            if (!_initialized)
            {
                var asserterService = _serviceProvider.GetRequiredService<AsserterService>();
                Asserter.Initialize(asserterService);
                _initialized = true;
            }
        }
    }
}
