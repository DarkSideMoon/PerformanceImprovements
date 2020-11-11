using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi.Services.Storage
{
    public interface IStorage<TItem>
    {
        Task Set(string key, TItem item);

        Task<TItem> Get(string key);

        Task<TItem> GetOrSet(string key, TItem item);

        Task SetBatch(string key, IEnumerable<TItem> items);

        Task<IEnumerable<TItem>> GetBatch(IEnumerable<string> keys);
    }
}
