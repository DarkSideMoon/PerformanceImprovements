using Newtonsoft.Json;

namespace MovieApi.Configuration
{
    public class RedisConfiguration
    {
        [JsonProperty("connectionString")]
        public string ConnectionString { get; set; }
    }
}
