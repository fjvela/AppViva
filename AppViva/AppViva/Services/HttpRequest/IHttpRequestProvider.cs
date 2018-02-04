using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppViva.Services
{
    public interface IHttpRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri, CancellationToken cancelToken = default(CancellationToken), string token = "");

        Task PostAsync(string uri, object data, CancellationToken cancelToken = default(CancellationToken), string token = "", string header = "");

        Task<TResult> PostAsync<TResult>(string uri, object data, CancellationToken cancelToken = default(CancellationToken), string token = "", string header = "");

        Task DeleteAsync(string uri, CancellationToken cancelToken = default(CancellationToken), string token = "");
    }
}
