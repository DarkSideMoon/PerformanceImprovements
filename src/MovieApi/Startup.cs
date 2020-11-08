using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieApi.Configuration;
using MovieApi.Infrastructure;
using Serilog;

namespace MovieApi
{
    public class Startup
    {
        private readonly ILogger _logger = Log.ForContext<Startup>();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            _logger.Information("Start configure dependecies...");

            var serviceConfig = Configuration.Get<ServiceConfiguration>();

            services.AddControllers();

            // Registers health checks services
            services.AddHealthChecks();

            // Configure swagger
            services.AddSwaggerService();

            // Add memory cache
            services.AddInMemoryStorage(serviceConfig.Redis.ConnectionString);
        }

        public void Configure(IApplicationBuilder app, IHostApplicationLifetime applicationLifetime)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHealthChecks("/healthz");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Service API V1");
            });

            app.UseHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains("health")
            });

            applicationLifetime.ApplicationStarted.Register(() =>
            {
                _logger.Information("Service has been started");
            });
        }
    }
}
