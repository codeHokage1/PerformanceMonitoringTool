using Confluent.Kafka;
using System.Text.Json;

namespace PerformanceMonitoringTool.ApiService
{
    public class ConsumerService : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly string _topic;
        private readonly ILogger<ConsumerService> _logger;

        public ConsumerService(IConfiguration configuration, ILogger<ConsumerService> logger)
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
            }

            _consumer.Close();
        }

        private async Task ProcessMessageAsync(HeartbeatMessage message)
        {
            // _logger.LogInformation($"KAFKA - Received message: {message}");
            _logger.LogInformation($" KAFKA - Received heartbeat from {message.ApplicationName} at {message.Timestamp}");
            // Add your message processing logic here
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
        public DateTime Timestamp { get; set; }
    }
}
