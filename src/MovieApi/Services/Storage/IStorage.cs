using MovieModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi.Services.Storage
{
    public interface IStorage<TItem> where TItem : IStorageKey
    {
        Task Set(TItem item);

        Task<TItem> Get(string key);

        Task<TItem> GetOrSet(string key, TItem item);

        Task SetBatch(IEnumerable<TItem> items);

        Task SetPipeline(IEnumerable<TItem> items);

        Task<IEnumerable<TItem>> GetBatch(IEnumerable<string> keys);

        string BuildKey(string key);
    }
}
