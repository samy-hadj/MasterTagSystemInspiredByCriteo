namespace MasterTagSystem.Models
{
    public class UserInfo
    {
        public int age { get; set; }                  // Âge de l'utilisateur
        public string? location { get; set; }         // Localisation de l'utilisateur
        public Preferences? preferences { get; set; } // Préférences utilisateur
    }
}