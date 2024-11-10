using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MasterTagSystem.Models;

namespace MasterTagSystem.Services
{
    public class KafkaConsumerService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly TagService _tagService;

        public KafkaConsumerService(IConfiguration configuration, TagService tagService)
        {
            _configuration = configuration;
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "json-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            _consumer.Subscribe("json-requests");
            _tagService = tagService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = _consumer.Consume(cancellationToken);
                        Console.WriteLine($"Message reçu : {consumeResult.Message.Value}");

                        var tagData = System.Text.Json.JsonSerializer.Deserialize<TagModel>(consumeResult.Message.Value);
                        if (tagData != null)
                        {
                            _tagService.ValidateTag(tagData);
                        }
                    }
                    catch (OperationCanceledException) { }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erreur lors de la consommation de message : {ex.Message}");
                    }
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Close();
            return Task.CompletedTask;
        }
    }
}
