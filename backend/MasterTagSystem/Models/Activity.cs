namespace MasterTagSystem.Models
{
    /// <summary>
    /// Represents the activity of a user.
    /// </summary>
    public class Activity
    {
        /// <summary>
        /// Gets or sets the last login time of the user.
        /// </summary>
        public string? lastLogin { get; set; }        // Last login of the user

        /// <summary>
        /// Gets or sets the number of pages visited by the user.
        /// </summary>
        public int pagesVisited { get; set; }         // Number of pages visited

        /// <summary>
        /// Gets or sets the actions performed by the user (click, scroll, etc.).
        /// </summary>
        public string? actions { get; set; }          // User actions (click, scroll, etc.)
    }
}