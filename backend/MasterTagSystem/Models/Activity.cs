namespace MasterTagSystem.Models
{
    public class Activity
    {
        public string? lastLogin { get; set; }        // Dernière connexion de l'utilisateur
        public int pagesVisited { get; set; }         // Nombre de pages visitées
        public string? actions { get; set; }          // Actions de l'utilisateur (clic, scroll, etc.)
    }
}