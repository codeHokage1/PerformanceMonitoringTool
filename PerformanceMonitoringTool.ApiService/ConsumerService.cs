using Confluent.Kafka;

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
                    await ProcessMessageAsync(consumeResult.Message.Value);
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

        private async Task ProcessMessageAsync(string message)
        {
            _logger.LogInformation($"KAFKA - Received message: {message}");
            // Add your message processing logic here
        }

        public override void Dispose()
        {
            _consumer.Dispose();
            base.Dispose();
        }
    }
}
