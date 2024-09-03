using Confluent.Kafka;
using PerformanceMonitoringTool.ApiService.Models.Entities;
using PerformanceMonitoringTool.ApiService.Repositories;
using System.Text.Json;

namespace PerformanceMonitoringTool.ApiService
{
    public class ConsumerService : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly string _topic;
        private readonly ILogger<ConsumerService> _logger;
        private readonly IServiceProvider _serviceProvider;

        // private readonly IMonitoredApps _monitoredAppsRepository;

        public ConsumerService(
            IConfiguration configuration, 
            ILogger<ConsumerService> logger,
            // IMonitoredApps monitoredAppsRepository
            IServiceProvider serviceProvider
        )
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"],
                GroupId = configuration["Kafka:GroupId"],
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
            _topic = configuration["Kafka:ConsumerTopic"];
            _logger = logger;
            _serviceProvider = serviceProvider;
            //_monitoredAppsRepository = monitoredAppsRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe(_topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(stoppingToken);
                    var heartbeat = JsonSerializer.Deserialize<HeartbeatMessage>(consumeResult.Message.Value);
                    await ProcessMessageAsync(heartbeat);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error consuming message");
                }

                // Add a small delay to prevent tight looping
                await Task.Delay(100, stoppingToken);
            }

            _consumer.Close();
        }

        private async Task ProcessMessageAsync(HeartbeatMessage message)
        {
            // _logger.LogInformation($"KAFKA - Received message: {message}");
            _logger.LogInformation($" KAFKA - Received heartbeat from {message.ApplicationName} (ID: {message.ApplicationId}) at {message.Timestamp}");

            // Add information from message to database
            var appName = message.ApplicationName;
            var appId = message.ApplicationId;

            using (var scope = _serviceProvider.CreateScope())
            {
                var monitoredAppRepo = scope.ServiceProvider.GetRequiredService<IMonitoredApps>();
                // Find the app in the database
                var monitoredApp = await monitoredAppRepo.GetMonitoredAppByAppIdAsync(appId);
                if (monitoredApp == null)
                {
                    _logger.LogInformation($"KAFKA - Application {appName} not found in database.");
                    return;
                }

                // Update the last heartbeat time and isOnline
                var updatedApp = await monitoredAppRepo.UpdateMonitoredAppAsync(monitoredApp.Id, new MonitoredApp
                {
                    AppId = monitoredApp.AppId,
                    Name = monitoredApp.Name,
                    IsOnline = true,
                    LastChecked = message.Timestamp
                });
                if (updatedApp != null)
                {
                    _logger.LogInformation($"KAFKA - Application {appName} updated in database.");
                }
            }

        }

        public override void Dispose()
        {
            _consumer.Dispose();
            base.Dispose();
        }
    }

    public class HeartbeatMessage
    {
        public string ApplicationName { get; set; }
        public string ApplicationId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
