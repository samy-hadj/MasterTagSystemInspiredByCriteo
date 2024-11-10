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
}
