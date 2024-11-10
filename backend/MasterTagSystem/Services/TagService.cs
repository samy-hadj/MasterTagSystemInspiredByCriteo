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
                // Validation des champs
                if (string.IsNullOrEmpty(tag.id))
                {
                    Console.WriteLine("Validation échouée : ID manquant.");
                    return false;
                }

                if (string.IsNullOrEmpty(tag.destinationUrl) || !Uri.IsWellFormedUriString(tag.destinationUrl, UriKind.Absolute))
                {
                    Console.WriteLine("Validation échouée : URL invalide.");
                    return false;
                }

                if (string.IsNullOrEmpty(tag.trackingData))
                {
                    Console.WriteLine("Validation échouée : données de suivi manquantes.");
                    return false;
                }

                if (tag.clickCount == null || tag.sessionId == null)
                {
                    Console.WriteLine("Validation échouée : informations de session ou clickCount manquantes.");
                    return false;
                }

                _jsonCollection.InsertOne(tag);
                Console.WriteLine("Insertion réussie : " + tag);

                // Envoie les nouvelles données JSON aux clients connectés
                _hubContext.Clients.All.SendAsync("ReceiveJsonUpdate", tag);

                return true;
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
