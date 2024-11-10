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
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<TagService>();  // Enregistrer TagService

            // Enregistrer KafkaConsumerService en tant que service hébergé
            services.AddHostedService<KafkaConsumerService>();

            // Configuration MongoDB
            var mongoConnectionString = "mongodb://localhost:27017";
            var mongoClient = new MongoClient(mongoConnectionString);
            var mongoDatabase = mongoClient.GetDatabase("CriteoProject");
            services.AddSingleton(mongoDatabase);

            // Configuration de SignalR
            services.AddSignalR();

            // Configuration CORS pour autoriser l'accès depuis le frontend Angular
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowSpecificOrigin");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<JsonHub>("/jsonHub"); // Mapping pour le hub
            });
        }
    }
}
