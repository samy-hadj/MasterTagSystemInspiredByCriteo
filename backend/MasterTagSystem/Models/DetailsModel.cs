namespace MasterTagSystem.Models
{
    /// <summary>
    /// Represents the details model containing user information and activity information.
    /// </summary>
    public class DetailsModel
    {
        /// <summary>
        /// Gets or sets the user information.
        /// </summary>
        public UserInfo? userInfo { get; set; }       // User information

        /// <summary>
        /// Gets or sets the user activity information.
        /// </summary>
        public Activity? activity { get; set; }       // User activity information
    }
}