using MongoDB.Driver;

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

            // Configuration CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:4200") // Remplacez par l'URL de votre frontend
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
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
            });
        }
    }
}
