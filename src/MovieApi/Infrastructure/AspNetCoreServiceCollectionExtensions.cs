using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MovieApi.Services.Storage;
using MovieModel;
using MovieApi.Services;

namespace MovieApi.Infrastructure
{
    public static class AspNetCoreServiceCollectionExtensions
    {
        public static IServiceCollection AddHealthChecksService(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck("base", () => HealthCheckResult.Healthy("Ok"));

            return services;
        }

        public static IServiceCollection AddClientServices(this IServiceCollection services)
        {
            services.AddTransient<IMovieClient, MovieClient>();

            return services;
        }

        public static IServiceCollection AddInMemoryStorage(this IServiceCollection services, string redisConnectionString)
        {
            // Add redis cache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
            });

            services.AddSingleton<IRedisConnectionFactory, RedisConnectionFactory>();
            services.AddSingleton<IStorage<Movie>>(
                x => new RedisStorage<Movie>(x.GetRequiredService<IDistributedCache>(), x.GetRequiredService<IRedisConnectionFactory>()));
            return services;
        }
    }
}
