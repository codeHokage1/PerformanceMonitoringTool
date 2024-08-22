namespace PerformanceMonitoringTool.ApiService
{
    public class BackgroundMonitoringService : BackgroundService
    {
        private readonly ILogger<BackgroundMonitoringService> _logger;

        public BackgroundMonitoringService(ILogger<BackgroundMonitoringService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background monitoring service is starting.");

            stoppingToken.Register(() =>
                _logger.LogInformation("Background monitoring service is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Background monitoring service at {time}", DateTimeOffset.Now);
                    await Task.Delay(5000, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred in the background monitoring service.");
                }
            }

            _logger.LogInformation("Background monitoring service has stopped.");
        }
    }
}
