using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using MasterTagSystem.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MasterTagSystem.Services
{
    public class KafkaConsumerService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly TagService _tagService;
        private readonly string _topic = "json-requests";

        public KafkaConsumerService(IConfiguration configuration, TagService tagService)
        {
            _configuration = configuration;
            var config = new ConsumerConfig
            {
                BootstrapServers = _configuration.GetValue<string>("KAFKA_BOOTSTRAP_SERVERS") ?? "localhost:9092",
                GroupId = "json-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false // Permet un contrôle manuel des offsets
            };
            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            _tagService = tagService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe(_topic);

            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var messages = new List<ConsumeResult<Ignore, string>>();
                        for (int i = 0; i < 100; i++)  // Récupère jusqu'à 100 messages en batch
                        {
                            var result = _consumer.Consume(TimeSpan.FromMilliseconds(100));
                            if (result != null)
                                messages.Add(result);
                            else
                                break;
                        }

                        // Traiter tous les messages récupérés
                        foreach (var message in messages)
                        {
                            try
                            {
                                var tagData = JsonSerializer.Deserialize<TagModel>(message.Message.Value);
                                if (tagData != null)
                                {
                                    _tagService.ValidateTag(tagData);
                                }
                                _consumer.Commit(message); // Marquer l'offset comme traité
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Erreur de traitement du message : {ex.Message}");
                            }
                        }
                    }
                    catch (OperationCanceledException) { }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erreur lors de la consommation : {ex.Message}");
                        await Task.Delay(5000, cancellationToken); // Temporisation avant nouvelle tentative
                    }
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Unsubscribe();
            _consumer.Close();
            _consumer.Dispose();
            return Task.CompletedTask;
        }
    }
}
