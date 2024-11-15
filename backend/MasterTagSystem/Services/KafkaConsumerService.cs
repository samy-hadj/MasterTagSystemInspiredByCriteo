using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace MasterTagSystem.Services
{
    public class KafkaConsumerService : IHostedService
    {
        private readonly ILogger<KafkaConsumerService> _logger;
        private readonly IConsumer<string, string> _consumer;
        private readonly string _topic = "json-requests";
        private bool _running;

        private readonly IConfiguration _configuration;
        private readonly TagService _tagService;

        public KafkaConsumerService(ILogger<KafkaConsumerService> logger, IConfiguration configuration, TagService tagService)
        {
            _logger = logger;
            _configuration = configuration;
            _tagService = tagService;

            var bootstrapServers = _configuration.GetValue<string>("KAFKA_BOOTSTRAP_SERVERS") 
                ?? Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVERS") 
                ?? "kafka:9092";

            // Configuration de Kafka
            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = "mastertagsystem-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<string, string>(config).Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _running = true;

            // Démarrage de la tâche de consommation
            Task.Run(() => StartConsuming(cancellationToken), cancellationToken);

            return Task.CompletedTask;
        }

        private async Task StartConsuming(CancellationToken cancellationToken)
        {
            while (_running && !cancellationToken.IsCancellationRequested)
            {
                try
                {
                    // Abonnement au topic
                    _consumer.Subscribe(_topic);
                    var result = _consumer.Consume(cancellationToken);

                    // Traiter le message
                    _logger.LogInformation($"Message reçu : {result.Message.Value}");
                    // Appel du service TagService ou autre traitement ici
                    var tagModel = JsonSerializer.Deserialize<MasterTagSystem.Models.TagModel>(result.Message.Value);
                    _tagService.ValidateTag(tagModel);

                }
                catch (Exception ex)
                {
                    _logger.LogError($"Erreur lors de la consommation de Kafka : {ex.Message}");
                    await Task.Delay(5000, cancellationToken); // Attendre avant de réessayer
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _running = false;
            _consumer.Close();
            _consumer.Dispose();
            return Task.CompletedTask;
        }
    }
}