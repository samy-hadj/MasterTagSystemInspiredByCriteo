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
    /// <summary>
    /// Kafka consumer service that listens to a Kafka topic and processes JSON messages.
    /// Implements the IHostedService interface to integrate with the application's lifecycle.
    /// </summary>
    public class KafkaConsumerService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly TagService _tagService;
        private readonly string _topic = "json-requests"; // Kafka topic to consume messages from

        /// <summary>
        /// Initializes a new instance of the <see cref="KafkaConsumerService"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration object.</param>
        /// <param name="tagService">Service used to validate tags.</param>
        public KafkaConsumerService(IConfiguration configuration, TagService tagService)
        {
            _configuration = configuration;

            // Configuration for Kafka consumer
            var config = new ConsumerConfig
            {
                BootstrapServers = _configuration.GetValue<string>("KAFKA_BOOTSTRAP_SERVERS") ?? "localhost:9092", // Kafka brokers
                GroupId = "json-consumer-group", // Consumer group identifier
                AutoOffsetReset = AutoOffsetReset.Earliest, // Start consuming from the earliest available message
                EnableAutoCommit = false // Manual offset commit for better control
            };

            // Build the Kafka consumer
            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            _tagService = tagService;
        }

        /// <summary>
        /// Starts the Kafka consumer service.
        /// </summary>
        /// <param name="cancellationToken">Token to signal cancellation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe(_topic); // Subscribe to the Kafka topic

            // Start the consumer loop in a separate task
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var messages = new List<ConsumeResult<Ignore, string>>();

                        // Retrieve up to 100 messages in batch
                        for (int i = 0; i < 100; i++)
                        {
                            var result = _consumer.Consume(TimeSpan.FromMilliseconds(100));
                            if (result != null)
                                messages.Add(result);
                            else
                                break;
                        }

                        // Process all retrieved messages
                        foreach (var message in messages)
                        {
                            try
                            {
                                var tagData = JsonSerializer.Deserialize<TagModel>(message.Message.Value); // Deserialize JSON to TagModel
                                if (tagData != null)
                                {
                                    _tagService.ValidateTag(tagData); // Validate the tag data
                                }
                                _consumer.Commit(message); // Mark the offset as processed
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Message processing error: {ex.Message}"); // Log processing errors
                            }
                        }
                    }
                    catch (OperationCanceledException) { } // Expected exception when stopping the service
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Consumption error: {ex.Message}"); // Log consumption errors
                        await Task.Delay(5000, cancellationToken); // Wait before retrying
                    }
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Stops the Kafka consumer service.
        /// </summary>
        /// <param name="cancellationToken">Token to signal cancellation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Unsubscribe(); // Unsubscribe from the Kafka topic
            _consumer.Close(); // Gracefully close the consumer
            _consumer.Dispose(); // Dispose of consumer resources
            return Task.CompletedTask;
        }
    }
}
