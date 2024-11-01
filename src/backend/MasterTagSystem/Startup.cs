using MasterTagSystem.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MasterTagSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Configure les services de l'application
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<TagService>(); // Injection de TagService
            
            // Configuration CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:4200") // Remplacez par l'URL de votre frontend
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });
        }

        // Configure le pipeline HTTP de l'application
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            // Appliquez la politique CORS
            app.UseCors("AllowSpecificOrigin");

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
