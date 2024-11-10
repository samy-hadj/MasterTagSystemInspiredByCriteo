namespace MasterTagSystem.Models
{
    public class TagModel
    {
        public string? id { get; set; }               // ID aléatoire unique
        public string? destinationUrl { get; set; }   // URL de destination
        public string? trackingData { get; set; }     // Données de suivi
        public int? clickCount { get; set; }          // Compteur de clics (nullable)
        public string? sessionId { get; set; }        // Identifiant de session (nullable)
        public string? referrer { get; set; }         // URL de référence (nullable)

        public DetailsModel? details { get; set; }    // Champ imbriqué pour les informations détaillées
    }
}
