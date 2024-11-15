using MongoDB.Driver;
using MasterTagSystem.Models;
using Microsoft.AspNetCore.SignalR;
using MasterTagSystem.Hubs;
using System;
using System.Collections.Generic;

namespace MasterTagSystem.Services
{
    /// <summary>
    /// Service for managing tags, including validation, storage in MongoDB, and broadcasting updates via SignalR.
    /// </summary>
    public class TagService
    {
        private readonly IMongoCollection<TagModel> _jsonCollection; // MongoDB collection for storing tags
        private readonly IHubContext<JsonHub> _hubContext; // SignalR hub for broadcasting updates

        /// <summary>
        /// Initializes a new instance of the <see cref="TagService"/> class.
        /// </summary>
        /// <param name="database">MongoDB database instance.</param>
        /// <param name="hubContext">SignalR hub context for broadcasting messages.</param>
        public TagService(IMongoDatabase database, IHubContext<JsonHub> hubContext)
        {
            _jsonCollection = database.GetCollection<TagModel>("jsons");
            _hubContext = hubContext;
        }

        /// <summary>
        /// Validates and processes a tag. If valid, stores it in MongoDB and broadcasts it to all clients.
        /// </summary>
        /// <param name="tag">The tag to validate and process.</param>
        /// <returns>True if the tag is valid and processed successfully; otherwise, false.</returns>
        public bool ValidateTag(TagModel tag)
        {
            try
            {
                // Generate a random number between 0 and 1. Validate only if it's <= 10%.
                if (new Random().NextDouble() <= 0.1) // 10% chance
                {
                    // Validate tag data
                    if (string.IsNullOrEmpty(tag.id) ||
                        string.IsNullOrEmpty(tag.destinationUrl) ||
                        !Uri.IsWellFormedUriString(tag.destinationUrl, UriKind.Absolute) ||
                        string.IsNullOrEmpty(tag.trackingData) ||
                        tag.clickCount == null ||
                        tag.sessionId == null)
                    {
                        // Console.WriteLine("Invalid data ignored.");
                        return false;
                    }

                    // Insert into MongoDB if validated
                    _jsonCollection.InsertOne(tag);
                    Console.WriteLine("Valid data! Successfully inserted: " + tag);

                    // Broadcast to clients
                    _hubContext.Clients.All.SendAsync("ReceiveJsonUpdate", tag);
                    return true;
                }
                else
                {
                    // Ignore unvalidated messages
                    // Console.WriteLine("Message ignored.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during insertion: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Retrieves all tags from the MongoDB collection.
        /// </summary>
        /// <returns>A list of all tags.</returns>
        public List<TagModel> GetAllTags()
        {
            try
            {
                return _jsonCollection.Find(FilterDefinition<TagModel>.Empty).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving tags: {ex.Message}");
                return new List<TagModel>();
            }
        }

        /// <summary>
        /// Updates an existing tag in the MongoDB collection.
        /// </summary>
        /// <param name="id">The ID of the tag to update.</param>
        /// <param name="updatedTag">The updated tag data.</param>
        /// <returns>True if the tag was updated successfully; otherwise, false.</returns>
        public bool UpdateTag(string id, TagModel updatedTag)
        {
            try
            {
                var filter = Builders<TagModel>.Filter.Eq(t => t.id, id); // Filter by ID
                var update = Builders<TagModel>.Update
                    .Set(t => t.id, updatedTag.id)
                    .Set(t => t.destinationUrl, updatedTag.destinationUrl)
                    .Set(t => t.trackingData, updatedTag.trackingData)
                    .Set(t => t.clickCount, updatedTag.clickCount)
                    .Set(t => t.sessionId, updatedTag.sessionId)
                    .Set(t => t.referrer, updatedTag.referrer);

                var result = _jsonCollection.UpdateOne(filter, update);

                if (result.ModifiedCount > 0)
                {
                    // Notify clients of the update
                    _hubContext.Clients.All.SendAsync("ReceiveJsonUpdate", updatedTag);
                    return true;
                }
                else
                {
                    Console.WriteLine("No tag updated.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating tag: {ex.Message}");
                return false;
            }
        }
    }
}
