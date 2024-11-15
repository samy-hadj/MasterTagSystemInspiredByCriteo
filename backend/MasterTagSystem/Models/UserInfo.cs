namespace MasterTagSystem.Models
{
    /// <summary>
    /// Represents information about a user, including age, location, and preferences.
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// Gets or sets the user's age.
        /// </summary>
        public int age { get; set; } // User's age

        /// <summary>
        /// Gets or sets the user's location.
        /// </summary>
        public string? location { get; set; } // User's location

        /// <summary>
        /// Gets or sets the user's preferences.
        /// </summary>
        public Preferences? preferences { get; set; } // User's preferences
    }
}
