using MongoDB.Driver;
using MasterTagSystem.Hubs;

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
            services.AddScoped<TagService>();

            // Configuration MongoDB
            var mongoConnectionString = "mongodb://localhost:27017"; // Connexion à MongoDB
            var mongoClient = new MongoClient(mongoConnectionString);
            var mongoDatabase = mongoClient.GetDatabase("CriteoProject"); // Nom de la base de données

            // Test de la connexion MongoDB
            try
            {
                mongoClient.ListDatabases(); // Cette commande va tester la connexion
                Console.WriteLine("Connexion MongoDB réussie !");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur de connexion MongoDB : {ex.Message}");
            }

            services.AddSingleton(mongoDatabase); // Injection de la base MongoDB

            // Ajouter SignalR
            services.AddSignalR(); // Ajoutez SignalR ici

            // Configuration CORS pour autoriser l'accès depuis le frontend Angular
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:4200") // URL du frontend
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowCredentials()); // Autoriser les cookies
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
                endpoints.MapHub<JsonHub>("/jsonHub"); // Ajoutez le mapping pour le hub
            });
        }
    }
}
