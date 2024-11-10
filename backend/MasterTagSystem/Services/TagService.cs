using MongoDB.Driver;
using MasterTagSystem.Models;
using Microsoft.AspNetCore.SignalR;
using MasterTagSystem.Hubs;
using System;
using System.Collections.Generic;

namespace MasterTagSystem.Services
{
    public class TagService
    {
        private readonly IMongoCollection<TagModel> _jsonCollection;
        private readonly IHubContext<JsonHub> _hubContext;

        public TagService(IMongoDatabase database, IHubContext<JsonHub> hubContext)
        {
            _jsonCollection = database.GetCollection<TagModel>("jsons");
            _hubContext = hubContext;
        }

        public bool ValidateTag(TagModel tag)
        {
            try
            {
                // Générer un nombre aléatoire entre 0 et 1, et valider si c'est inférieur ou égal à 10%
                if (new Random().NextDouble() <= 0.1) // 10% de chance
                {
                    // Validation des données
                    if (string.IsNullOrEmpty(tag.id) ||
                        string.IsNullOrEmpty(tag.destinationUrl) ||
                        !Uri.IsWellFormedUriString(tag.destinationUrl, UriKind.Absolute) ||
                        string.IsNullOrEmpty(tag.trackingData) ||
                        tag.clickCount == null ||
                        tag.sessionId == null)
                    {
                        Console.WriteLine("Données non valides ignorées.");
                        return false;
                    }

                    // Insertion dans MongoDB si validé
                    _jsonCollection.InsertOne(tag);
                    Console.WriteLine("Insertion réussie : " + tag);

                    // Diffusion aux clients
                    _hubContext.Clients.All.SendAsync("ReceiveJsonUpdate", tag);
                    return true;
                }
                else
                {
                    // Ignorer les messages non validés
                    Console.WriteLine("Message ignoré.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'insertion : {ex.Message}");
                return false;
            }
        }

        public List<TagModel> GetAllTags()
        {
            try
            {
                return _jsonCollection.Find(FilterDefinition<TagModel>.Empty).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des tags : {ex.Message}");
                return new List<TagModel>();
            }
        }

        public bool UpdateTag(string id, TagModel updatedTag)
        {
            try
            {
                var filter = Builders<TagModel>.Filter.Eq(t => t.id, id);
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
                    _hubContext.Clients.All.SendAsync("ReceiveJsonUpdate", updatedTag);
                    return true;
                }
                else
                {
                    Console.WriteLine("Aucun tag mis à jour.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la mise à jour du tag : {ex.Message}");
                return false;
            }
        }
    }
}
