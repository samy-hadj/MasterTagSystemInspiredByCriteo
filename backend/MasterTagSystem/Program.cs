using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MasterTagSystem
{
    /// <summary>
    /// Entry point for the MasterTagSystem application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method that initializes and runs the application.
        /// </summary>
        /// <param name="args">Command-line arguments passed to the application.</param>
        public static void Main(string[] args)
        {
            // Create and run the Host for the application
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Configures and returns the host builder for the application.
        /// </summary>
        /// <param name="args">Command-line arguments passed to the application.</param>
        /// <returns>An <see cref="IHostBuilder"/> configured with default settings and the Startup class.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Specify the Startup class for application configuration
                    webBuilder.UseStartup<Startup>();
                });
    }
}
