using MongoDB.Driver;
using MasterTagSystem.Models;
using Microsoft.AspNetCore.SignalR;
using MasterTagSystem.Hubs;

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
            if (string.IsNullOrEmpty(tag.id) || !Uri.IsWellFormedUriString(tag.destinationUrl, UriKind.Absolute))
            {
                Console.WriteLine("Validation échouée : ID ou URL invalide");
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

    // Ajout de la fonction GetAllTags pour récupérer tous les JSONs
    public List<TagModel> GetAllTags()
    {
        try
        {
            return _jsonCollection.Find(FilterDefinition<TagModel>.Empty).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la récupération des tags : {ex.Message}");
            return new List<TagModel>(); // Retourne une liste vide en cas d'erreur
        }
    }

            // Nouvelle méthode pour mettre à jour un tag existant
        public bool UpdateTag(string id, TagModel updatedTag)
        {
            try
            {
                var filter = Builders<TagModel>.Filter.Eq(t => t.id, id);
                var update = Builders<TagModel>.Update
                    .Set(t => t.id, updatedTag.id)
                    .Set(t => t.destinationUrl, updatedTag.destinationUrl)
                    .Set(t => t.trackingData, updatedTag.trackingData);

                var result = _jsonCollection.UpdateOne(filter, update);

                if (result.ModifiedCount > 0)
                {
                    // Envoie les données mises à jour aux clients connectés
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
