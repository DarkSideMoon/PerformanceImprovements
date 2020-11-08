using Newtonsoft.Json;

namespace MovieApi.Configuration
{
    public class ServiceConfiguration
    {
        [JsonProperty("redis")]
        public RedisConfiguration Redis { get; set; }
    }
}
