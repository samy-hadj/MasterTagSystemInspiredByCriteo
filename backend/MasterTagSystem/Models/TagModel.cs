/// <summary>
/// Represents a tag model with various properties for tracking and session information.
/// </summary>
namespace MasterTagSystem.Models
{
    public class TagModel
    {
        /// <summary>
        /// Gets or sets the unique random ID.
        /// </summary>
        public string? id { get; set; }               // Unique random ID

        /// <summary>
        /// Gets or sets the destination URL.
        /// </summary>
        public string? destinationUrl { get; set; }   // Destination URL

        /// <summary>
        /// Gets or sets the tracking data.
        /// </summary>
        public string? trackingData { get; set; }     // Tracking data

        /// <summary>
        /// Gets or sets the click count (nullable).
        /// </summary>
        public int? clickCount { get; set; }          // Click count (nullable)

        /// <summary>
        /// Gets or sets the session ID (nullable).
        /// </summary>
        public string? sessionId { get; set; }        // Session ID (nullable)

        /// <summary>
        /// Gets or sets the referrer URL (nullable).
        /// </summary>
        public string? referrer { get; set; }         // Referrer URL (nullable)

        /// <summary>
        /// Gets or sets the nested field for detailed information.
        /// </summary>
        public DetailsModel? details { get; set; }    // Nested field for detailed information
    }
}