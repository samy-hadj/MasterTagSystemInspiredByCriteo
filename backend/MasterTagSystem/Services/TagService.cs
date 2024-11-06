using MongoDB.Driver;
using MasterTagSystem.Models;

public class TagService
{
    private readonly IMongoCollection<TagModel> _jsonCollection;

    public TagService(IMongoDatabase database)
    {
        _jsonCollection = database.GetCollection<TagModel>("jsons"); // Utilise la collection "jsons"
    }

    public bool ValidateTag(TagModel tag)
    {
        try
        {
            // Exemple de validation simple : vérifier que l'ID et l'URL sont valides
            if (string.IsNullOrEmpty(tag.Id) || !Uri.IsWellFormedUriString(tag.DestinationUrl, UriKind.Absolute))
            {
                Console.WriteLine("Validation échouée : ID ou URL invalide");
                Console.WriteLine(tag);
                return false; // Si la validation échoue, retourne false
            }

            // Insertion dans MongoDB
            _jsonCollection.InsertOne(tag);
            Console.WriteLine("Insertion réussie : " + tag);
            return true; // Retourne true si l'insertion réussie
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de l'insertion : {ex.Message}");
            return false; // Retourne false en cas d'erreur
        }
    }
}
