using MongoDB.Driver;
using MasterTagSystem.Hubs;
using MasterTagSystem.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace MasterTagSystem
{
    /// <summary>
    /// The startup class configures the services and the HTTP request pipeline for the application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class with the specified configuration.
        /// </summary>
        /// <param name="configuration">Application configuration settings.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures services for dependency injection in the application.
        /// </summary>
        /// <param name="services">The service collection to which services are added.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add controllers support for API endpoints
            services.AddControllers();

            // Register TagService as a singleton service
            services.AddSingleton<TagService>();

            // Register KafkaConsumerService as a hosted background service
            services.AddHostedService<KafkaConsumerService>();

            // MongoDB configuration
            var mongoConnectionString = Configuration.GetValue<string>("MONGO_CONNECTION_STRING") ?? "mongodb://localhost:27017";
            var mongoClient = new MongoClient(mongoConnectionString);
            var mongoDatabase = mongoClient.GetDatabase("CriteoProject");
            services.AddSingleton(mongoDatabase); // Inject MongoDB database instance

            // SignalR configuration for real-time communication
            services.AddSignalR();

            // CORS configuration to allow access from the Angular frontend
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .WithOrigins(Configuration["CORS_ORIGINS"]?.Split(",") ?? new[] { "http://localhost:4200" })
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder to configure middleware.</param>
        /// <param name="env">The hosting environment information.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Use developer exception page if in development mode
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enforce HTTPS for all requests
            app.UseHttpsRedirection();

            // Configure routing
            app.UseRouting();

            // Apply CORS policy to requests
            app.UseCors("AllowSpecificOrigin");

            // Enable authorization middleware
            app.UseAuthorization();

            // Map endpoints for controllers and SignalR hubs
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Map controller endpoints
                endpoints.MapHub<JsonHub>("/jsonHub"); // Map SignalR hub for real-time communication
            });
        }
    }
}
