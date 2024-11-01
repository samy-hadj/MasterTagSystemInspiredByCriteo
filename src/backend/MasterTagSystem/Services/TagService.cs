using MasterTagSystem.Models;

namespace MasterTagSystem.Services
{
    public class TagService
    {
        public bool ValidateTag(Tag tag)
        {
            // Exemple de validation : vérifier que l'ID et l'URL sont valides
            return !string.IsNullOrEmpty(tag.Id) && Uri.IsWellFormedUriString(tag.DestinationUrl, UriKind.Absolute);
        }
    }
}
