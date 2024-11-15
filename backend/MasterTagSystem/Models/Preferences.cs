/// <summary>
/// Represents user preferences such as theme and language.
/// </summary>
namespace MasterTagSystem.Models
{
    public class Preferences
    {
        /// <summary>
        /// Gets or sets the preferred theme (dark or light).
        /// </summary>
        public string? theme { get; set; }            // Theme preference (dark or light)

        /// <summary>
        /// Gets or sets the preferred language.
        /// </summary>
        public string? language { get; set; }         // Preferred language
    }
}