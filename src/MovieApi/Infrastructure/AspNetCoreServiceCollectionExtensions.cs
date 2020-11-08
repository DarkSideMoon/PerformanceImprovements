using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MovieApi.Services.Storage;
using MovieModel;

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

        public static IServiceCollection AddInMemoryStorage(this IServiceCollection services, string redisConnectionString)
        {
            // Add redis cache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
            });

            services.AddSingleton<IStorage<Movie>>(x => new RedisStorage<Movie>(x.GetRequiredService<IDistributedCache>()));
            return services;
        }
    }
}
