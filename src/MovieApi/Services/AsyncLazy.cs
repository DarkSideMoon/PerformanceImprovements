using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MovieApi.Services
{
    /// <summary>
    /// Interesting implementation Async Lazy 
    /// Source: https://devblogs.microsoft.com/pfxteam/asynclazyt/
    /// </summary>
    /// <example>
    /// static AsyncLazy<string> m_data = new AsyncLazy</string>(async delegate
    ///    {
    ///        WebClient client = new WebClient();
    ///        return (await client.DownloadStringTaskAsync(someUrl)).ToUpper();
    ///    });
    /// </example>
    /// <typeparam name="T"></typeparam>
    public class AsyncLazy<T> : Lazy<Task<T>>
    {
        public AsyncLazy(Func<T> valueFactory) :
            base(() => Task.Factory.StartNew(valueFactory))
        {
        }

        public AsyncLazy(Func<Task<T>> taskFactory) :
            base(() => Task.Factory.StartNew(() => taskFactory()).Unwrap())
        { 
        }

        public TaskAwaiter<T> GetAwaiter() => Value.GetAwaiter();
    }
}
